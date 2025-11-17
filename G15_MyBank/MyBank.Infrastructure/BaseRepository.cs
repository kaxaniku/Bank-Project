using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyBank.Domain.Interfaces;
using MyBank.Infrastructure.Interfaces;

namespace MyBank.Infrastructure;

internal abstract class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet;
    private bool _disposed = false;

    protected BaseRepository(BankDbContext context)
    {
        _dbSet = context.Set<T>();
    }

    protected void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(_disposed, this.GetType());

    public T? GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }

    public async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate)
    {

        return await _dbSet.Where(predicate).ToListAsync();
    }

    public void Insert(T entity)
    {
        _dbSet.Add(entity);
    }

    public async Task InsertAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public void Delete(T entity)
    {
        if (entity is IDisable dEntity && !dEntity.Activity.IsActive)
            throw new DbUpdateConcurrencyException("Entity is already disabled.");
        
        _dbSet.Remove(entity);
    }

    public Task DeleteAsync(T entity)
    {
        if (entity is IDisable dEntity && !dEntity.Activity.IsActive)
            throw new DbUpdateConcurrencyException("Entity is already disabled.");

        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                //We don't have managed resources to dispose in this base class
            }

            _disposed = true;
        }
    }

    ~BaseRepository() => Dispose(false);
}
