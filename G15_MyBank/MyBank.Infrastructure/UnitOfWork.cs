using Microsoft.EntityFrameworkCore.Storage;
using MyBank.Infrastructure.Interfaces;

namespace MyBank.Infrastructure;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly BankDbContext _context;
    private IDbContextTransaction? _transaction;
    private bool _disposed;

    private readonly Lazy<IAccountRepository> _account;
    private readonly Lazy<ICardRepository> _card;
    private readonly Lazy<ICityRepository> _city;
    private readonly Lazy<ICountryRepository> _country;
    private readonly Lazy<ICustomerRepository> _customer;
    private readonly Lazy<ITransactionRepository> _bankTransaction;

    public IAccountRepository AccountRepository => CheckDisposedAndGet(_account);

    public ICardRepository CardRepository => CheckDisposedAndGet(_card);
    public ICityRepository CityRepository => CheckDisposedAndGet(_city);
    public ICountryRepository CountryRepository => CheckDisposedAndGet(_country);
    public ICustomerRepository CustomerRepository => CheckDisposedAndGet(_customer);
    public ITransactionRepository TransactionRepository => CheckDisposedAndGet(_bankTransaction);

    public UnitOfWork(BankDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));

        _account = new Lazy<IAccountRepository>(() => new AccountRepository(_context));
        _card = new Lazy<ICardRepository>(() => new CardRepository(_context));
        _city = new Lazy<ICityRepository>(() => new CityRepository(_context));
        _country = new Lazy<ICountryRepository>(() => new CountryRepository(_context));
        _customer = new Lazy<ICustomerRepository>(() => new CustomerRepository(_context));
        _bankTransaction = new Lazy<ITransactionRepository>(() => new TransactionRepository(_context));
    }

    public int SaveChanges()
    {
        ThrowIfDisposed();
        return _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        ThrowIfDisposed();
        return await _context.SaveChangesAsync();
    }

    public void BeginTransaction()
    {
        ThrowIfDisposed();
        if (_transaction != null)
            throw new ArgumentException("Transaction has already started");

        _transaction = _context.Database.BeginTransaction();
    }

    public async Task BeginTransactionAsync()
    {
        ThrowIfDisposed();
        if (_transaction != null)
            throw new ArgumentException("Transaction has already started");

        _transaction = await _context.Database.BeginTransactionAsync();

    }

    public void Commit()
    {
        if (_transaction == null)
            throw new ArgumentException("Transaction has not started");

        _transaction?.Commit();
        _transaction?.Dispose();
        _transaction = null;
    }

    public async Task CommitAsync()
    {
        if (_transaction == null)
            throw new ArgumentException("Transaction has not started");

        await _transaction.CommitAsync();
        await _transaction.DisposeAsync();
        _transaction = null;

    }

    public void Rollback()
    {
        if (_transaction == null)
            throw new ArgumentException("Transaction has not started");

        _transaction?.Rollback();
        _transaction?.Dispose();
        _transaction = null;
    }

    public async Task RollbackAsync()
    {
        if (_transaction == null)
            throw new ArgumentException("Transaction has not started");

        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync();
        _transaction = null;

    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private T CheckDisposedAndGet<T>(Lazy<T> lazy)
    {
        ThrowIfDisposed();
        return lazy.Value;
    }

    private void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            _transaction?.Dispose();
            _transaction = null;

            if (_account.IsValueCreated)
                AccountRepository.Dispose();

            if (_card.IsValueCreated)
                CardRepository.Dispose();

            if (_city.IsValueCreated)
                CityRepository.Dispose();

            if (_country.IsValueCreated)
                CountryRepository.Dispose();

            if (_customer.IsValueCreated)
                CustomerRepository.Dispose();

            if (_bankTransaction.IsValueCreated)
                TransactionRepository.Dispose();

        }

        _disposed = true;
    }

    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(_disposed, this.GetType());

    ~UnitOfWork() => Dispose(false);
}
