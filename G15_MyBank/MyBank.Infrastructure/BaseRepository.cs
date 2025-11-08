using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyBank.Domain.Interfaces;
using MyBank.Infrastructure.Interfaces;

namespace MyBank.Infrastructure;
internal abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet;

    protected BaseRepository(BankDbContext context)
    {
        _dbSet = context.Set<T>();
    }

    public T? GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }

    public void Insert(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        if (entity is IDisable DEntity && DEntity.Activity.IsActive == false)
        {
           throw new DbUpdateConcurrencyException("Entity is already disabled.");
        }
        _dbSet.Remove(entity);
    }
}
