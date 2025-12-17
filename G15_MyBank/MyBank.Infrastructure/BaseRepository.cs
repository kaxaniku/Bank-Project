using System.IO;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyBank.Domain.Interfaces;
using MyBank.Application.Interfaces.Repositories;
using MyBank.Domain;

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

    public IQueryable<T> Query(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        ThrowIfDisposed();

        IQueryable<T> query = _dbSet;
        includes.ForEach(include => query = query.Include(include));

        return query.Where(predicate);
    }

    public IQueryable<T> Query(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, params Expression<Func<T, object>>[] includes)
    {
        ThrowIfDisposed();

        if (pageNumber < 1)
            throw new ArgumentException("Page number must be >= 1.", nameof(pageNumber));

        const int defaultPageSize = 10;
        if (pageSize < 1) pageSize = defaultPageSize;

        int skip = (pageNumber - 1) * pageSize;

        return Query(predicate, includes)
            .Skip(skip)
            .Take(pageSize);
    }

    public async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes)
    {
        ThrowIfDisposed();

        IQueryable<T> query = _dbSet;
        foreach (var include in includes)
            query = query.Include(include);

        return await query.Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes)
    {
        ThrowIfDisposed();

        if (pageNumber < 1)
            throw new ArgumentException("Page number must be >= 1.", nameof(pageNumber));

        const int defaultPageSize = 10;
        if (pageSize < 1) pageSize = defaultPageSize;

        int skip = (pageNumber - 1) * pageSize;

        IQueryable<T> query = _dbSet;
        foreach (var include in includes)
            query = query.Include(include);

        return await query
            .Where(predicate)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
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

        }
        

        _disposed = true;
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (!_disposed)
        {

            _disposed = true;
        }
    }
    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(_disposed, GetType());

    #endregion

    #region Finalizer

    ~BaseRepository() => Dispose(false);

    #endregion
}
