using Microsoft.EntityFrameworkCore.Storage;
using MyBank.Infrastructure.Interfaces;

namespace MyBank.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly BankDbContext _context;
    private IDbContextTransaction? _transaction;

    private readonly Lazy<IAccountRepository> _account;
    private readonly Lazy<ICardRepository> _card;
    private readonly Lazy<ICityRepository> _city;
    private readonly Lazy<ICountryRepository> _country;
    private readonly Lazy<ICustomerRepository> _customer;
    private readonly Lazy<ITransactionRepository> _bankTransaction;

    public IAccountRepository AccountRepository => _account.Value;
    public ICardRepository CardRepository => _card.Value;
    public ICityRepository CityRepository => _city.Value;
    public ICountryRepository CountryRepository => _country.Value;
    public ICustomerRepository CustomerRepository => _customer.Value;
    public ITransactionRepository TransactionRepository => _bankTransaction.Value;

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
        return _context.SaveChanges();
    }

    public void BeginTransaction()
    {
        if (_transaction != null)
            throw new ArgumentException("Transaction has already started");

        _transaction = _context.Database.BeginTransaction();
    }

    public void Commit()
    {
        if (_transaction == null)
            throw new ArgumentException("Transaction has not started");

        _transaction?.Commit();
        _transaction?.Dispose();
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

    public void Dispose()
    {
        _transaction?.Dispose();
    }
}
