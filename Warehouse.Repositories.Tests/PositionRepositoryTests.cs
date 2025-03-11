using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories.Tests;

public class PositionRepositoryTests : BaseRepositoryTests<Position>
{
    public IPositionRepository Repository => new UnitOfWork(_connection!).PositionRepository;

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Position position = new()
        {
            Name = "Test Position",
            Description = "Test Description",
            Salary = 50000
        };

        int id = (int)Repository.Insert(position);
        Position? result = Repository.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(position.Name, result!.Name);
        Assert.AreEqual(position.Description, result!.Description);
        Assert.AreEqual(position.Salary, result!.Salary);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        Position position = new()
        {
            Name = null,
            Description = "Test Description",
            Salary = 50000
        };

        Assert.Throws<SqlException>(() => Repository.Insert(position));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        Position? current = Repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = "Updated " + current.Name;
        current.Description = "Updated " + current.Description;
        current.Salary = 60000;
        Repository.Update(current);

        Position? updated = Repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.Name, updated!.Name);
        Assert.AreEqual(current.Description, updated!.Description);
        Assert.AreEqual(current.Salary, updated!.Salary);
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        Position? current = Repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = null;
        current!.Description = "Updated Description";

        Assert.Throws<SqlException>(() => Repository.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Position? current = Repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        Repository.Delete(Constants.DeleteTestId);
        Position? deleted = Repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Position? current = Repository.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        Repository.Delete(Constants.DeleteTestId2);
        Position? deleted = Repository.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => Repository.Delete(Constants.DeleteTestId2));
    }
}
