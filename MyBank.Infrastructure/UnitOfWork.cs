using MyBank.Application.Interfaces;
using MyBank.Infrastructure.Interfaces;
using MyBank.Infrastructure.Repositories;

namespace MyBank.Infrastructure
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly BankDbContext _context;
        private bool _disposed = false;

        public ITransactionRepository Transactions { get; }

        public UnitOfWork(BankDbContext context)
        {
            _context = context;

            Transactions = new TransactionRepository(_context);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));
        }

        public async Task<int> SaveChangesAsync()
        {
            ThrowIfDisposed();
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                _disposed = true;

                if (Transactions is IAsyncDisposable asyncRepo)
                    await asyncRepo.DisposeAsync();
                else if (Transactions is IDisposable syncRepo)
                    syncRepo.Dispose();

                await _context.DisposeAsync();
            }
        }
    }
}
