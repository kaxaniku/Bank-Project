using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Services.Interfaces.Repositories;

namespace Warehouse.Repositories.Tests;

public class ContractRepositoryTests : BaseRepositoryTests<Contract>
{
    private IContractRepository? _repository;

    [SetUp]
    public void Setup()
    {
        _repository = _unitOfWork!.ContractRepository;
    }

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

        int id = (int)_repository!.Insert(contract);
        Contract? result = _repository!.Get(id);

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

        Assert.Throws<SqlException>(() => _repository!.Insert(contract));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        Contract? current = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = "Updated " + current.Name;
        current.Description = "Updated " + current.Description;
        current.CustomerId = 2;
        current.EmployeeId = 2;
        current.Price = 200;
        _repository!.Update(current);

        Contract? updated = _repository!.Get(Constants.UpdateTestId);
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
        Contract? current = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = null;
        current.Description = "Updated " + current.Description;
        current.CustomerId = 2;
        current.EmployeeId = 2;
        current.Price = 200;

        Assert.Throws<SqlException>(() => _repository!.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Contract? current = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        _repository!.Delete(Constants.DeleteTestId);
        Contract? deleted = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Contract? current = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        _repository!.Delete(Constants.DeleteTestId2);
        Contract? deleted = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => _repository!.Delete(Constants.DeleteTestId2));
    }

    [Test]
    public void TestQuery()
    {
        IEnumerable<Contract> contracts = _repository!.Query(c => c.IsActive == true && c.Price > 300);

        Assert.IsNotNull(contracts);
        Assert.IsNotEmpty(contracts);

        foreach (var contract in contracts)
        {
            Assert.IsTrue(contract.IsActive, $"Contract {contract.Name} is not active.");
            Assert.Greater(contract.Price, 300, $"Contract {contract.Name} does not have a price greater than 300.");
        }
    }

}