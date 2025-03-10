using Microsoft.Data.SqlClient;
using Warehouse.DTO;

namespace Warehouse.Repositories.Tests;

public class SlotRepositoryTests : BaseRepositoryTests<Slot>
{
    [Test]
    public void TestInsert_ShouldInsert()
    {
        SlotRepository repository = new(_connection!);
        Slot slot = new()
        {
            StorageId = 1,
            SlotCode = "D1"
        };

        int id = (int)repository.Insert(slot);
        Slot? result = repository.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(slot.StorageId, result!.StorageId);
        Assert.AreEqual(slot.SlotCode, result!.SlotCode);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        SlotRepository repository = new(_connection!);
        Slot slot = new()
        {
            StorageId = 0, // Invalid ID, should fail
            SlotCode = "B2"
        };

        Assert.Throws<SqlException>(() => repository.Insert(slot));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        SlotRepository repository = new(_connection!);
        Slot? current = repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        repository.Delete(Constants.DeleteTestId);
        Slot? deleted = repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        SlotRepository repository = new(_connection!);
        Slot? current = repository.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        repository.Delete(Constants.DeleteTestId2);
        Slot? deleted = repository.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => repository.Delete(Constants.DeleteTestId2));
    }
}
