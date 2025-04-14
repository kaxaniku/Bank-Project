using Warehouse.DTO;
using Warehouse.Services.Exceptions;
using Warehouse.Services.Interfaces.Repositories;
using Warehouse.Services.Interfaces.Services;
using Warehouse.Services.Models;

namespace Warehouse.Services;

public sealed class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public void AddUser(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        user.UserId = (int)_unitOfWork.UserRepository.Insert(user);
    }

    public void EditUser(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        _unitOfWork.UserRepository.Update(user);
    }

    public void DeleteUser(int id)
    {
        _unitOfWork.UserRepository.Delete(id);
    }

    public IEnumerable<User> GetUsers(string? username = "")
    {
        return _unitOfWork.UserRepository.Query(x => x.IsActive && x.Username.StartsWith(username ?? ""));
    }

    public User? GetUser(int id)
    {
        return _unitOfWork.UserRepository.Get(id);
    }

    public int LoginUser(string username, string password)
    {
        ArgumentNullException.ThrowIfNull(username);
        ArgumentNullException.ThrowIfNull(password);

        int result = _unitOfWork.UserRepository.LoginUser(username, password);
        if (result == -1)
            throw new LoginException(username);
        return result;
    }
}