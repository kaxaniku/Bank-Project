using Warehouse.DTO;

namespace Warehouse.Repositories.Interfaces;

public interface ICityRepository
{
    City? Get(int id);
    IEnumerable<City> Query();
    int Insert(City value);
    void Update(City value);
    void Delete(int id);
}