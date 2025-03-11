using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories.Tests;

public class UserRepositoryTests : BaseRepositoryTests<User>
{
    public IUserRepository Repository => new UnitOfWork(_connection!).UserRepository;

    [Test]
    public void TestInsert_ShouldInsert()
    {
        byte[] pass = {1, 1, 1, 1};
        User user = new()
        {
            UserId = 4,
            Username = "Test UserName",
            Password = pass,
            UserRole = 1
        };

        int id = (int)Repository.Insert(user);
        User? result = Repository.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(user.Username, result!.Username);
        Assert.AreEqual(user.Password, result!.Password);
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

        Assert.Throws<SqlException>(() => Repository.Insert(user));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        User? current = Repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        byte[] pass = { 1, 1, 1, 2 };
        current!.Username = "Updated " + current.Username;
        current!.UserRole = 2;
        current!.Password = pass;
 
        Repository.Update(current);

        User? updated = Repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.Username, updated!.Username);
        Assert.AreEqual(current.UserRole, updated!.UserRole);
        Assert.AreEqual(current.Password, updated!.Password);
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        User? current = Repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Username = "Updated " + current.Username;
        current!.UserRole = 1;
        current!.Password = null;

        Assert.Throws<SqlException>(() => Repository.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        User? current = Repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        Repository.Delete(Constants.DeleteTestId);
        User? deleted = Repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        User? current = Repository.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        Repository.Delete(Constants.DeleteTestId2);
        User? deleted = Repository.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => Repository.Delete(Constants.DeleteTestId2));
    }
}