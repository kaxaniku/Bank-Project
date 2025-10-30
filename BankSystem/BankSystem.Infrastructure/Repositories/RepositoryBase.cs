using BankSystem.Application.Common.Interfaces.Repositories;
using BankSystem.Domain.Common;
using BankSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankSystem.Infrastructure.Repositories;
public class RepositoryBase<T>(BankSystemDbContext context) : IRepositoryBase<T> where T : BaseEntity
{
    protected readonly BankSystemDbContext Context = context;
    protected readonly DbSet<T> DbSet = context.Set<T>();

    public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    public virtual async Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual IQueryable<T> Query(Expression<Func<T, bool>> predicate)
    {
        return DbSet.Where(predicate).AsQueryable();
    }

    public virtual IQueryable<T> Query()
    {
        return DbSet.AsQueryable();
    }

    public virtual async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = DbSet;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }

    public virtual async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await DbSet.AddRangeAsync(entities, cancellationToken);
    }

    public virtual void Update(T entity)
    {
        DbSet.Update(entity);
    }

    public virtual void UpdateRange(IEnumerable<T> entities)
    {
        DbSet.UpdateRange(entities);
    }

    public virtual void Remove(T entity)
    {
        DbSet.Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        DbSet.RemoveRange(entities);
    }

    public void SoftDelete(T entity)
    {
        entity.SoftDelete();
        
        DbSet.Update(entity);
    }

    public void SoftDeleteRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            entity.SoftDelete();
        }

        DbSet.UpdateRange(entities);
    }

    public async Task<bool> SoftDeleteByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entity == null)
        {
            return false;
        }

        SoftDelete(entity);

        return true;
    }

    public void Restore(T entity)
    {
        entity.Restore();
        DbSet.Update(entity);
    }

    public void RestoreRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            entity.Restore();
        }

        DbSet.UpdateRange(entities);
    }

    public async Task<bool> RestoreByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entity == null || entity.IsActive)
        {
            return false;
        }

        Restore(entity);
        return true;
    }
}