using Microsoft.EntityFrameworkCore;
using MyBank.Application.Interfaces.Repositories;
using MyBank.Domain.Interfaces;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyBank.Infrastructure
{
    internal abstract class BaseRepository<T> : IRepository<T> where T : class 
    {
        private readonly MyBankDbContext _context;
        private readonly DbSet<T> _dbSet;
        private bool _isDisposed;

        protected BaseRepository(MyBankDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();
        }

        public T? GetById(int id)
        {
            ThrowIfDisposed();
            var entity = _dbSet.Find(id);
            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} not found.");

            return entity;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            ThrowIfDisposed();
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            return entity;
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            ThrowIfDisposed();
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return _dbSet.Where(predicate);
        }

        public async Task<List<T>> QueryAsync(Expression<Func<T, bool>> predicate)
        {
            ThrowIfDisposed();
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return await _dbSet.Where(predicate).ToListAsync();
        }

        public void Insert(T entity)
        {
            ThrowIfDisposed();
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public async Task InsertAsync(T entity)
        {
            ThrowIfDisposed();
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            ThrowIfDisposed();
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            ThrowIfDisposed();
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            if (entity is IDisable DEntity && DEntity.Activity.IsActive == false)
            {
                throw new DbUpdateConcurrencyException("Entity is already disabled.");
            }
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity is IDisable DEntity && DEntity.Activity.IsActive == false)
            {
                throw new DbUpdateConcurrencyException("Entity is already disabled.");
            }
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
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

        protected void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {

            }

            _isDisposed = true;
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
            }
        }

        private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(_isDisposed, GetType());

        ~BaseRepository() => Dispose(false);
    }
}
