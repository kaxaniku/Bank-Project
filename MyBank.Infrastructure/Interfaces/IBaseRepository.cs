using System.Linq.Expressions;

namespace MyBank.Infrastructure.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);

        void Update(T entity);
        void Remove(T entity);

        IQueryable<T> Query(Expression<Func<T, bool>> predicate);
    }
}
