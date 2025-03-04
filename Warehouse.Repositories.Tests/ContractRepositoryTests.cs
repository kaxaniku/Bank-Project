using Warehouse.DTO;

namespace Warehouse.Repositories.Tests;

public class ContractRepositoryTests : BaseRepositoryTests<Contract>
{

    [Test]
    public void TestInsert_ShouldInsert()
    {

        ContractRepository repository = new(_connection!);
        Contract contract = new()
        {
            Name = "Test Contract",
            Description = "Test Description",
            CustomerId = 1,
            EmployeeId = 1,
            Price = 100
        };

        int id = (int)repository.Insert(contract);
        Contract? result = repository.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(contract.Name, result!.Name);
        Assert.AreEqual(contract.Description, result!.Description);
        Assert.AreEqual(contract.CustomerId, result!.CustomerId);
        Assert.AreEqual(contract.EmployeeId, result!.EmployeeId);
        Assert.AreEqual(contract.Price, result!.Price);
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        ContractRepository repository = new(_connection!);
        Contract? current = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = "Updated " + current.Name;
        current.Description = "Updated " + current.Description;
        repository.Update(current);

        Contract? updated = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.Name, updated!.Name);
        Assert.AreEqual(current.Description, updated!.Description);
        Assert.AreEqual(current.CustomerId, updated!.CustomerId);
        Assert.AreEqual(current.EmployeeId, updated!.EmployeeId);
        Assert.AreEqual(current.Price, updated!.Price);
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        ContractRepository repository = new(_connection!);
        Contract? current = repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        repository.Delete(Constants.DeleteTestId);
        Contract? deleted = repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }
}