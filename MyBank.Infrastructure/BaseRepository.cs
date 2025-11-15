using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MyBank.Infrastructure.Repositories
{
    internal abstract class BaseRepository<T> : IDisposable where T : class
    {
        protected readonly BankDbContext _context;
        private bool _disposed;

        protected BaseRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            ThrowIfDisposed();
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            ThrowIfDisposed();
            return await _context.Set<T>().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            ThrowIfDisposed();
            await _context.Set<T>().AddAsync(entity);
        }

        public Task UpdateAsync(T entity)
        {
            ThrowIfDisposed();
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            ThrowIfDisposed();
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }


        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            ThrowIfDisposed();
            return _context.Set<T>().Where(predicate);
        }

        protected void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(BaseRepository<T>));
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
            }
        }
    }
}
