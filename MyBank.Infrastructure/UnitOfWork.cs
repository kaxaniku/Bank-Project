using MyBank.Application.Interfaces;
using MyBank.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MyBank.Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
    {
        private readonly MyBankDbContext _context;
        private bool _disposed;

        public ITransactionRepository Transactions { get; }

        public UnitOfWork(MyBankDbContext context)
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // dispose repositories
                    if (Transactions is IDisposable repo)
                        repo.Dispose();

                    // dispose DbContext
                    _context.Dispose();
                }

                _disposed = true;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                // dispose repositories async if possible
                if (Transactions is IAsyncDisposable asyncRepo)
                    await asyncRepo.DisposeAsync();
                else if (Transactions is IDisposable repo)
                    repo.Dispose();

                // dispose DbContext asynchronously
                await _context.DisposeAsync();

                _disposed = true;
            }

            GC.SuppressFinalize(this);
        }
    }
}
