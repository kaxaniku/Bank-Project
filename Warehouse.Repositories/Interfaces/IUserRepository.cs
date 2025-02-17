using Warehouse.DTO;

namespace Warehouse.Repositories.Interfaces;

public interface IUserRepository
{
    User? Get(int id);
    IEnumerable<User> Query();
    int Insert(User value);
    void Update(User value);
    void Delete(User value);
}