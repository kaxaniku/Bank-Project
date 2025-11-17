using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        public virtual T? GetById(Guid id)
        {
            ThrowIfDisposed();
            return _context.Set<T>().Find(id);
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            ThrowIfDisposed();
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            ThrowIfDisposed();
            return _context.Set<T>().ToList();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            ThrowIfDisposed();
            return await _context.Set<T>().ToListAsync();
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            ThrowIfDisposed();
            return _context.Set<T>().Where(predicate).ToList();
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            ThrowIfDisposed();
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual void Add(T entity)
        {
            ThrowIfDisposed();
            _context.Set<T>().Add(entity);
        }

        public virtual async Task AddAsync(T entity)
        {
            ThrowIfDisposed();
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            ThrowIfDisposed();
            _context.Set<T>().AddRange(entities);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            ThrowIfDisposed();
            await _context.Set<T>().AddRangeAsync(entities);
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

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            ThrowIfDisposed();
            _context.Set<T>().RemoveRange(entities);
        }

        public void Dispose()
        {
            _disposed = true;
        }

        public ValueTask DisposeAsync()
        {
            _disposed = true;
            return ValueTask.CompletedTask;
        }
    }
}
