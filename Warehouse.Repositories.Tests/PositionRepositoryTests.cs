using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories.Tests;

public class PositionRepositoryTests : BaseRepositoryTests<Position>
{
    private IPositionRepository? _repository;

    [SetUp]
    public void Setup()
    {
        _repository = _unitOfWork!.PositionRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Position position = new()
        {
            Name = "Test Position",
            Description = "Test Description",
            Salary = 50000
        };

        int id = (int)_repository!.Insert(position);
        Position? result = _repository!.Get(id);

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

        Assert.Throws<SqlException>(() => _repository!.Insert(position));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        Position? current = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = "Updated " + current.Name;
        current.Description = "Updated " + current.Description;
        current.Salary = 60000;
        _repository!.Update(current);

        Position? updated = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.Name, updated!.Name);
        Assert.AreEqual(current.Description, updated!.Description);
        Assert.AreEqual(current.Salary, updated!.Salary);
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        Position? current = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = null;
        current!.Description = "Updated Description";

        Assert.Throws<SqlException>(() => _repository!.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Position? current = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        _repository!.Delete(Constants.DeleteTestId);
        Position? deleted = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Position? current = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        _repository!.Delete(Constants.DeleteTestId2);
        Position? deleted = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => _repository!.Delete(Constants.DeleteTestId2));
    }

    [Test]
    public void TestQuery()
    {
        IEnumerable<Position> positions = _repository!.Query(p => p.Salary > 4000);

        Assert.IsNotNull(positions);
        
        foreach (var position in positions)
        {
            Assert.Greater(position.Salary, 4000, $"Position {position.Name} does not have a salary greater than 40000.");
        }
    }

}
