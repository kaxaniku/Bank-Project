using Microsoft.EntityFrameworkCore;
using MyBank.Infrastructure.Interfaces;

namespace MyBank.Infrastructure.Repositories
{
    internal abstract class BaseRepository<T> : IDisposable, IAsyncDisposable
        where T : class
    {
        protected readonly BankDbContext _context;
        private bool _disposed;

        protected BaseRepository(BankDbContext context)
        {
            _context = context;
        }

        protected void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            ThrowIfDisposed();
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            ThrowIfDisposed();
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task AddAsync(T entity)
        {
            ThrowIfDisposed();
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            ThrowIfDisposed();
            _context.Set<T>().Update(entity);
        }

        public virtual void Remove(T entity)
        {
            ThrowIfDisposed();
            _context.Set<T>().Remove(entity);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
            }
        }

        public ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                _disposed = true;
            }

            return ValueTask.CompletedTask;
        }
    }
}
