using Warehouse.DTO;

namespace Warehouse.Services.Interfaces.Services;

public interface IUserService
{
    void AddUser(User user);
    void EditUser(User user);
    void DeleteUser(int id);
    IEnumerable<User> GetUsers(string? username = "");
    User? GetUser(int id);
    int LoginUser(string username, string password);
}