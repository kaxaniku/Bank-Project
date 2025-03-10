using Microsoft.Data.SqlClient;
using Warehouse.DTO;

namespace Warehouse.Repositories.Tests;

public class UserRepositoryTests : BaseRepositoryTests<User>
{

    [Test]
    public void TestInsert_ShouldInsert()
    {

        UserRepository repository = new(_connection!);
        byte[] pass = {1, 1, 1, 1};
        User user = new()
        {
            UserId = 4,
            Username = "Test UserName",
            Password = pass,
            UserRole = 1
        };

        int id = (int)repository.Insert(user);
        User? result = repository.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(user.Username, result!.Username);
        Assert.AreEqual(user.Password, result!.Password);
        Assert.AreEqual(user.UserRole, result!.UserRole);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        UserRepository repository = new(_connection!);
        User user = new()
        {
            UserId = 5,
            Username = "TestUserName",
            Password = null,
            UserRole = 1
        };

        Assert.Throws<SqlException>(() => repository.Insert(user));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        UserRepository repository = new(_connection!);
        User? current = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        byte[] pass = { 1, 1, 1, 2 };
        current!.Username = "Updated " + current.Username;
        current!.UserRole = 2;
        current!.Password = pass;
 
        repository.Update(current);

        User? updated = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.Username, updated!.Username);
        Assert.AreEqual(current.UserRole, updated!.UserRole);
        Assert.AreEqual(current.Password, updated!.Password);
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        UserRepository repository = new(_connection!);
        User? current = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Username = "Updated " + current.Username;
        current!.UserRole = 1;
        current!.Password = null;

        Assert.Throws<SqlException>(() => repository.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        UserRepository repository = new(_connection!);
        User? current = repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        repository.Delete(Constants.DeleteTestId);
        User? deleted = repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        UserRepository repository = new(_connection!);
        User? current = repository.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        repository.Delete(Constants.DeleteTestId2);
        User? deleted = repository.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => repository.Delete(Constants.DeleteTestId2));
    }
}