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

        public IAccountRepository AccountRepository => CheckIfDispose(_account);
        public ICardRepository CardRepository => CheckIfDispose(_card);
        public ICityRepository CityRepository => CheckIfDispose(_city);
        public ICountryRepository CountryRepository => CheckIfDispose(_country);
        public ICustomerRepository CustomerRepository => CheckIfDispose(_customer);
        public ITransactionRepository TransactionRepository => CheckIfDispose(_transaction);
        
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
        
        public async Task<int> SavechangesAsync(CancellationToken token)
        {
            return await _context.SaveChangesAsync(token);
        }

        public void BeginTransaction()
        {
            if(_dbTransaction != null)
                throw new InvalidOperationException("A transaction is already in progress.");

            _dbTransaction = _context.Database.BeginTransaction();
        }
        
        public async Task BeginTrasactionAsync(CancellationToken token)
        {
            if (_dbTransaction != null)
                throw new InvalidOperationException("A transaction is already in progress.");

            _dbTransaction = await _context.Database.BeginTransactionAsync(token);
        }

        public void CommitTransaction()
        {
            if(_dbTransaction == null)
                throw new InvalidOperationException("No transaction in progress to commit.");

            _dbTransaction.Commit();
            _dbTransaction.Dispose();
            _dbTransaction = null;
        }
        
        public async Task CommitTransactionAsync(CancellationToken token)
        {
            if (_dbTransaction == null)
                throw new InvalidOperationException("No transaction in progress to commit.");

            await _dbTransaction.CommitAsync(token);
            await _dbTransaction.DisposeAsync();
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
        
        public async Task RollbackTransactionAsyn(CancellationToken token)
        {
            if (_dbTransaction == null)
                throw new InvalidOperationException("No transaction in progress to rollback.");

            await _dbTransaction.RollbackAsync(token);
            await _dbTransaction.DisposeAsync();
            _dbTransaction = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();
            GC.SuppressFinalize(this);
        }

        private T CheckIfDispose<T>(Lazy<T> lazy)
        {
            ThrowIfDisposed();
            return lazy.Value;
        }

        private void Dispose(bool disposing)
        {

            if (_isDisposed) return;

            if (disposing)
            {
                if(_dbTransaction != null)
                {
                    _dbTransaction.DisposeAsync();
                    _dbTransaction = null;
                }

                if (_account.IsValueCreated)
                    _account.Value.Dispose();

                if (_card.IsValueCreated)
                    _card.Value.Dispose();

                if (_city.IsValueCreated)
                    _city.Value.Dispose();

                if (_country.IsValueCreated)
                    _country.Value.Dispose();

                if (_customer.IsValueCreated)
                    _customer.Value.Dispose();

                if (_transaction.IsValueCreated)
                    _transaction.Value.Dispose();
            }

            _isDisposed = true;
        }

        private async ValueTask DisposeAsyncCore()
        {
            if (!_isDisposed)
            {
                if (_dbTransaction != null)
                {
                    await _dbTransaction.DisposeAsync();
                    _dbTransaction = null;
                }

                if (_account.IsValueCreated)
                    await _account.Value.DisposeAsync();

                if (_card.IsValueCreated)
                    await _card.Value.DisposeAsync();

                if (_city.IsValueCreated)
                    await _city.Value.DisposeAsync();

                if (_country.IsValueCreated)
                    await _country.Value.DisposeAsync();

                if (_customer.IsValueCreated)
                    await _customer.Value.DisposeAsync();

                if (_transaction.IsValueCreated)
                    await _transaction.Value.DisposeAsync();
            }
        }

        protected void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(_isDisposed, this.GetType());

        ~UnitOfWork() => Dispose(false);
    }
}
