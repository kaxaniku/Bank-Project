using MyBank.Application.Interfaces;
using MyBank.Infrastructure.Interfaces;
using MyBank.Infrastructure.Repositories;

namespace MyBank.Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
    {
        private readonly BankDbContext _context;

        private bool _disposed;

        private IAccountRepository? _accounts;
        private ICardRepository? _cards;
        private ICityRepository? _cities;
        private ICountryRepository? _countries;
        private ICustomerRepository? _customers;
        private ITransactionRepository? _transactions;

        public UnitOfWork(BankDbContext context)
        {
            _context = context;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));
        }

        public IAccountRepository Accounts
        {
            get
            {
                ThrowIfDisposed();
                return _accounts ??= new AccountRepository(_context);
            }
        }

        public ICardRepository Cards
        {
            get
            {
                ThrowIfDisposed();
                return _cards ??= new CardRepository(_context);
            }
        }

        public ICityRepository Cities
        {
            get
            {
                ThrowIfDisposed();
                return _cities ??= new CityRepository(_context);
            }
        }

        public ICountryRepository Countries
        {
            get
            {
                ThrowIfDisposed();
                return _countries ??= new CountryRepository(_context);
            }
        }

        public ICustomerRepository Customers
        {
            get
            {
                ThrowIfDisposed();
                return _customers ??= new CustomerRepository(_context);
            }
        }

        public ITransactionRepository Transactions
        {
            get
            {
                ThrowIfDisposed();
                return _transactions ??= new TransactionRepository(_context);
            }
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

        private static void DisposeRepository(IDisposable? repo)
        {
            repo?.Dispose();
        }

        private static async ValueTask DisposeRepositoryAsync(object? repo)
        {
            if (repo is IAsyncDisposable ad)
                await ad.DisposeAsync();
            else if (repo is IDisposable d)
                d.Dispose();
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            DisposeRepository(_accounts);
            DisposeRepository(_cards);
            DisposeRepository(_cities);
            DisposeRepository(_countries);
            DisposeRepository(_customers);
            DisposeRepository(_transactions);

            _context.Dispose();

            _disposed = true;
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposed)
                return;

            await DisposeRepositoryAsync(_accounts);
            await DisposeRepositoryAsync(_cards);
            await DisposeRepositoryAsync(_cities);
            await DisposeRepositoryAsync(_countries);
            await DisposeRepositoryAsync(_customers);
            await DisposeRepositoryAsync(_transactions);

            await _context.DisposeAsync();

            _disposed = true;
        }
    }
}
