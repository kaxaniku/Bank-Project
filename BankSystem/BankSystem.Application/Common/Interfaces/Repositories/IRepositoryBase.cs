using System.Linq.Expressions;

namespace BankSystem.Application.Common.Interfaces.Repositories;

public interface IRepositoryBase<T> where T : class
{
    void SoftDelete(T entity);
    void SoftDeleteRange(IEnumerable<T> entities);
    Task<bool> SoftDeleteByIdAsync(int id, CancellationToken cancellationToken = default);

    void Restore(T entity);
    void RestoreRange(IEnumerable<T> entities);
    Task<bool> RestoreByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    IQueryable<T> Query(Expression<Func<T, bool>> predicate);
    IQueryable<T> Query();

    Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}
