using Microsoft.Data.SqlClient;
using Warehouse.DTO;

namespace Warehouse.Repositories.Tests;

public class PositionRepositoryTests : BaseRepositoryTests<Position>
{
    [Test]
    public void TestInsert_ShouldInsert()
    {
        PositionRepository repository = new(_connection!);
        Position position = new()
        {
            Name = "Test Position",
            Description = "Test Description",
            Salary = 50000
        };

        int id = (int)repository.Insert(position);
        Position? result = repository.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(position.Name, result!.Name);
        Assert.AreEqual(position.Description, result!.Description);
        Assert.AreEqual(position.Salary, result!.Salary);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        PositionRepository repository = new(_connection!);
        Position position = new()
        {
            Name = null, // Name is required, should cause an error
            Description = "Test Description",
            Salary = 50000
        };

        Assert.Throws<SqlException>(() => repository.Insert(position));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        PositionRepository repository = new(_connection!);
        Position? current = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = "Updated " + current.Name;
        current.Description = "Updated " + current.Description;
        current.Salary = 60000;
        repository.Update(current);

        Position? updated = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.Name, updated!.Name);
        Assert.AreEqual(current.Description, updated!.Description);
        Assert.AreEqual(current.Salary, updated!.Salary);
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        PositionRepository repository = new(_connection!);
        Position? current = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = null; // Invalid update, should fail

        Assert.Throws<SqlException>(() => repository.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        PositionRepository repository = new(_connection!);
        Position? current = repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        repository.Delete(Constants.DeleteTestId);
        Position? deleted = repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        PositionRepository repository = new(_connection!);
        Position? current = repository.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        repository.Delete(Constants.DeleteTestId2);
        Position? deleted = repository.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => repository.Delete(Constants.DeleteTestId2));
    }
}
