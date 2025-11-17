using System.Linq.Expressions;

namespace MyBank.Infrastructure.Interfaces;
public interface IBaseRepository<T> where T : class
{
    T? GetById(int id);
    IQueryable<T> Query(Expression<Func<T, bool>> predicate);
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);

    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate);
    Task InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);

    void Dispose();
}
