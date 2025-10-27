using BankSystem.Application.Common.Interfaces.Repositories;
using BankSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace BankSystem.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly BankSystemDbContext _context;
    private IDbContextTransaction? _transaction;
    private bool _disposed;

    private readonly Lazy<ICustomerRepository> _customersRepository;
    private readonly Lazy<IAccountRepository> _accountsRepository;
    private readonly Lazy<ICardRepository> _cardsRepository;
    private readonly Lazy<ITransactionRepository> _transactionsRepository;

    public UnitOfWork(BankSystemDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));

        _customersRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(_context));
        _accountsRepository = new Lazy<IAccountRepository>(() => new AccountRepository(_context));
        _cardsRepository = new Lazy<ICardRepository>(() => new CardRepository(_context));
        _transactionsRepository = new Lazy<ITransactionRepository>(() => new TransactionRepository(_context));
    }

    public ICustomerRepository Customers => _customersRepository.Value;
    public IAccountRepository Accounts => _accountsRepository.Value;
    public ICardRepository Cards => _cardsRepository.Value;
    public ITransactionRepository Transactions => _transactionsRepository.Value;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        if (_transaction != null)
            throw new InvalidOperationException("A transaction is already in progress.");

        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        return _transaction;
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        if (_transaction == null)
            throw new InvalidOperationException("No active transaction to commit.");

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    private async Task DisposeTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;

        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        Dispose(disposing: false);
        GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            _transaction?.Dispose();
        }

        _disposed = true;
    }
}