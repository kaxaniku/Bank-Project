using System.Linq.Expressions;

namespace MyBank.Application.Interfaces.Repositories
{
    public interface IRepository<T> : IDisposable, IAsyncDisposable
    {
        T? GetById(int id);
        Task<T> GetByIdAsync(int id, CancellationToken token);
        IQueryable<T> Query(Expression<Func<T, bool>> predicate);
        Task<List<T>> QueryAsync(Expression<Func<T, bool>> predicate, CancellationToken token);
        void Insert(T entity);
        Task InsertAsync(T entity, CancellationToken token);
        void Update(T entity);
        Task UpdateAsync(T entity, CancellationToken token);
        void Delete(T entity);
        Task DeleteAsync(T entity, CancellationToken token);
    }
}
