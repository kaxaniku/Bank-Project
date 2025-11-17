using System.Linq.Expressions;

namespace MyBank.Application.Interfaces.Repositories
{
    public interface IRepository<T> : IDisposable
    {
        T? GetById(int id);
        Task<T> GetByIdAsync(int id);
        IQueryable<T> Query(Expression<Func<T, bool>> predicate);
        Task<List<T>> QueryAsync(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        Task InsertAsync(T entity);
        void Update(T entity);
        Task UpdateAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(T entity);
    }
}
