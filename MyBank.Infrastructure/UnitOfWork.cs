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

        public IAccountRepository Accounts =>
            _accounts ??= new AccountRepository(_context);

        public ICardRepository Cards =>
            _cards ??= new CardRepository(_context);

        public ICityRepository Cities =>
            _cities ??= new CityRepository(_context);

        public ICountryRepository Countries =>
            _countries ??= new CountryRepository(_context);

        public ICustomerRepository Customers =>
            _customers ??= new CustomerRepository(_context);

        public ITransactionRepository Transactions =>
            _transactions ??= new TransactionRepository(_context);

        public Task<int> SaveChangesAsync()
        {
            ThrowIfDisposed();
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (_disposed) return;

            DisposeRepository(_accounts);
            DisposeRepository(_cards);
            DisposeRepository(_cities);
            DisposeRepository(_countries);
            DisposeRepository(_customers);
            DisposeRepository(_transactions);

            _disposed = true;
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposed) return;

            await DisposeRepositoryAsync(_accounts);
            await DisposeRepositoryAsync(_cards);
            await DisposeRepositoryAsync(_cities);
            await DisposeRepositoryAsync(_countries);
            await DisposeRepositoryAsync(_customers);
            await DisposeRepositoryAsync(_transactions);

            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private static void DisposeRepository(object? repo)
        {
            if (repo is IDisposable d)
                d.Dispose();
        }

        private static async ValueTask DisposeRepositoryAsync(object? repo)
        {
            if (repo is IAsyncDisposable ad)
                await ad.DisposeAsync();
            else if (repo is IDisposable d)
                d.Dispose();
        }
    }
}
