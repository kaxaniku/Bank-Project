using Warehouse.DTO;

namespace Warehouse.Services.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    int LoginUser(string username, string password);
}