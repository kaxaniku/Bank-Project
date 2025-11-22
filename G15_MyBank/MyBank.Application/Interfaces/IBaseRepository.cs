using System.Linq.Expressions;

namespace MyBank.Application.Interfaces;
public interface IBaseRepository<T> where T : class
{
    T? GetById(int id);
    IQueryable<T> Query(Expression<Func<T, bool>> predicate);
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);

    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task InsertAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);

    void Dispose();

    ValueTask DisposeAsync();
}
