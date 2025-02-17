namespace Warehouse.Repositories.Interfaces;

public interface IRepository<T>
{
    T? Get(object id);
    IEnumerable<T> Query();
    object Insert(T value);
    void Update(T value);
    void Delete(object id);
}