using System.Data;
using System.Linq.Expressions;

namespace Warehouse.Repositories.Interfaces;

public interface IRepository<T>
{
    T? Get(object id);
    IEnumerable<T> Query(Expression<Func<T, bool>> predicate);
    object Insert(T value);
    void Update(T value);
    void Delete(object id);
    void SetTransaction(IDbTransaction transaction);
}