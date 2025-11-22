using System.IO;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyBank.Domain.Interfaces;
using MyBank.Application.Interfaces.Repositories;

namespace MyBank.Infrastructure;

internal abstract class BaseRepository<T> : IDisposable, IAsyncDisposable, IBaseRepository<T> where T : class
{
    #region Private and protected fields

    private readonly DbSet<T> _dbSet;
    private bool _disposed = false;

    #endregion

    #region Constructors

    protected BaseRepository(BankDbContext context)
    {
        _dbSet = context.Set<T>();
    }

    #endregion

    #region Public properties

    #endregion

    #region Public methods

    public T? GetById(int id)
    {
        ThrowIfDisposed();
        return _dbSet.Find(id);
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
    {
        ThrowIfDisposed();
        return _dbSet.Where(predicate);
    }

    public async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    public void Insert(T entity)
    {
        ThrowIfDisposed();
        _dbSet.Add(entity);
    }

    public async Task InsertAsync(T entity, CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Update(T entity)
    {
        ThrowIfDisposed();
        _dbSet.Update(entity);
    }

    public Task UpdateAsync(T entity)
    {
        ThrowIfDisposed();
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

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();
        GC.SuppressFinalize(this);
    }

    #endregion

    #region Private and protected methods

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            //We don't have managed resources to dispose in this base class
        }

        _disposed = true;
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (!_disposed)
        {
            //Nothing to dispose asynchronously in this base class

            _disposed = true;
        }
    }
    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(_disposed, GetType());

    #endregion

    #region Finalizer

    ~BaseRepository() => Dispose(false);

    #endregion
}
