using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories.Tests;

public class ContractRepositoryTests : BaseRepositoryTests<Contract>
{
    public IContractRepository Repository => new UnitOfWork(_connection!).ContractRepository;

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Contract contract = new()
        {
            Name = "Test Contract",
            Description = "Test Description",
            CustomerId = 1,
            EmployeeId = 1,
            Price = 100
        };

        int id = (int)Repository.Insert(contract);
        Contract? result = Repository.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(contract.Name, result!.Name);
        Assert.AreEqual(contract.Description, result!.Description);
        Assert.AreEqual(contract.CustomerId, result!.CustomerId);
        Assert.AreEqual(contract.EmployeeId, result!.EmployeeId);
        Assert.AreEqual(contract.Price, result!.Price);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        Contract contract = new()
        {
            Name = null,
            Description = "Test Description",
            CustomerId = 1,
            EmployeeId = 1,
            Price = 100
        };

        Assert.Throws<SqlException>(() => Repository.Insert(contract));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        Contract? current = Repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = "Updated " + current.Name;
        current.Description = "Updated " + current.Description;
        current.CustomerId = 2;
        current.EmployeeId = 2;
        current.Price = 200;
        Repository.Update(current);

        Contract? updated = Repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.Name, updated!.Name);
        Assert.AreEqual(current.Description, updated!.Description);
        Assert.AreEqual(current.CustomerId, updated!.CustomerId);
        Assert.AreEqual(current.EmployeeId, updated!.EmployeeId);
        Assert.AreEqual(current.Price, updated!.Price);
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        Contract? current = Repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = null;
        current.Description = "Updated " + current.Description;
        current.CustomerId = 2;
        current.EmployeeId = 2;
        current.Price = 200;

        Assert.Throws<SqlException>(() => Repository.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Contract? current = Repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        Repository.Delete(Constants.DeleteTestId);
        Contract? deleted = Repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Contract? current = Repository.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        Repository.Delete(Constants.DeleteTestId2);
        Contract? deleted = Repository.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => Repository.Delete(Constants.DeleteTestId2));
    }
}