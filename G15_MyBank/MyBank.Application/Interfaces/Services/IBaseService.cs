using MyBank.Domain;
using System.Linq.Expressions;

namespace MyBank.Application.Interfaces.Services
{
    internal interface IBaseService<T> where T : class
    {
        void Get(int id);
        Task GetAsync(int id);
        void Add(T entity);
        Task AddAsync(T entity);
        IQueryable<T> Query(Expression<Func<T, bool>> predicate);
        Task<List<T>> QueryAsync(Expression<Func<T, bool>> predicate);
        void Delete(T entity);
        Task DeleteAsync(T entity);
    }
}
