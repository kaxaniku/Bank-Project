using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Services.Interfaces.Repositories;

namespace Warehouse.Repositories.Tests;

public class UserRepositoryTests : BaseRepositoryTests<User>
{
    private IUserRepository? _repository;

    [SetUp]
    public void Setup()
    {
        _repository = _unitOfWork!.UserRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {
        User user = new()
        {
            UserId = 4,
            Username = "Test UserName",
            Password = "123Pass",
            UserRole = 1
        };

        int id = (int)_repository!.Insert(user);
        User? result = _repository!.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(user.Username, result!.Username);
        Assert.AreEqual(user.UserRole, result!.UserRole);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        User user = new()
        {
            UserId = 5,
            Username = "TestUserName",
            Password = null,
            UserRole = 1
        };

        Assert.Throws<SqlException>(() => _repository!.Insert(user));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        User? current = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Username = "Updated " + current.Username;
        current!.UserRole = 2;
        current!.Password = "123Pass++";

        _repository!.Update(current);

        User? updated = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.Username, updated!.Username);
        Assert.AreEqual(current.UserRole, updated!.UserRole);
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        User? current = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Username = "Updated " + current.Username;
        current!.UserRole = 1;
        current!.Password = null;

        Assert.Throws<SqlException>(() => _repository!.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        User? current = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        _repository!.Delete(Constants.DeleteTestId);
        User? deleted = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        User? current = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        _repository!.Delete(Constants.DeleteTestId2);
        User? deleted = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => _repository!.Delete(Constants.DeleteTestId2));
    }

    [Test]
    public void TestQuery_ShouldReturnUsersWithSpecificRole()
    {
        IEnumerable<User> users = _repository!.Query(user => user.UserRole == 1);

        Assert.IsNotNull(users);

        foreach (var user in users)
        {
            Assert.AreEqual(1, user.UserRole, $"User with Username {user.Username} does not have the expected UserRole.");
        }
    }

    [Test]
    public void TestLogin_ShouldLoginUser()
    {
        int result = _repository!.LoginUser("admin", "admin123");
        Assert.Greater(result, 0, "Login failed.");
    }

    [Test]
    public void TestLogout_ShouldNotLoginUser()
    {
        int result = _repository!.LoginUser("admin", "wrongpassword");
        Assert.AreEqual(-1, result, "Login should not be successful.");
    }
}