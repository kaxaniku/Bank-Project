using MyBank.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace MyBank.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyBankDbContext _context;
        private IDbContextTransaction? _dbTransaction;
        private bool _isDisposed;

        private readonly Lazy<IAccountRepository> _account;
        private readonly Lazy<ICardRepository> _card;
        private readonly Lazy<ICityRepository> _city;
        private readonly Lazy<ICountryRepository> _country;
        private readonly Lazy<ICustomerRepository> _customer;
        private readonly Lazy<ITransactionRepository> _transaction;

        public IAccountRepository AccountRepository => _account.Value;
        public ICardRepository CardRepository => _card.Value;
        public ICityRepository CityRepository => _city.Value;
        public ICountryRepository CountryRepository => _country.Value;
        public ICustomerRepository CustomerRepository => _customer.Value;
        public ITransactionRepository TransactionRepository => _transaction.Value;
        
        public UnitOfWork(MyBankDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _account = new Lazy<IAccountRepository>(() => new AccountRepository(_context));
            _card = new Lazy<ICardRepository>(() => new CardRepository(_context));
            _city = new Lazy<ICityRepository>(() => new CityRepository(_context));
            _country = new Lazy<ICountryRepository>(() => new CountryRepository(_context));
            _customer = new Lazy<ICustomerRepository>(() => new CustomerRepository(_context));
            _transaction = new Lazy<ITransactionRepository>(() => new TransactionRepository(_context));
        }

        public int SaveChanges()
        {
            ThrowIfDisposed();
            return _context.SaveChanges();
        }

        public void BeginTransaction()
        {
            if(_dbTransaction != null)
                throw new InvalidOperationException("A transaction is already in progress.");

            _dbTransaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if(_dbTransaction == null)
                throw new InvalidOperationException("No transaction in progress to commit.");

            _dbTransaction.Commit();
            _dbTransaction.Dispose();
            _dbTransaction = null;
        }

        public void RollbackTransaction()
        {
            if(_dbTransaction == null)
                throw new InvalidOperationException("No transaction in progress to rollback.");

            _dbTransaction.Rollback();
            _dbTransaction.Dispose();
            _dbTransaction = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {

            if (_isDisposed) return;

            if (disposing)
            {
                _dbTransaction?.Dispose();
                _dbTransaction = null;
            }

            _isDisposed = true;

        }

        private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(_isDisposed, this.GetType());

        ~UnitOfWork() => Dispose(false);
    }
}
