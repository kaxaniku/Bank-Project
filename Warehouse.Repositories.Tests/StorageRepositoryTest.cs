using Microsoft.Data.SqlClient;
using Warehouse.DTO;

namespace Warehouse.Repositories.Tests;

public class StorageRepositoryTests : BaseRepositoryTests<Tag>
{
    [Test]
    public void TestInsert_ShouldInsert()
    {
        StorageRepository repository = new(_connection!);
        Storage storage = new()
        {
            Name = "Test Storage",
            AddressLine1 = "Test AddressLine1",
            AddressLine2 = "Test AddressLine2",
            CityId = 1,
            Description = "Test Description"

        };
        int id = (int)repository.Insert(storage);
        Storage? result = repository.Get(id);
        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(storage.Name, result!.Name);
        Assert.AreEqual(storage.AddressLine1, result!.AddressLine1);
        Assert.AreEqual(storage.AddressLine2, result!.AddressLine2);
        Assert.AreEqual(storage.CityId, result!.CityId);
        Assert.AreEqual(storage.Description, result!.Description);

    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        StorageRepository repository = new(_connection!);
        Storage storage = new()
        {
            Name = null,
            AddressLine1 = "Test AddressLine1",
            AddressLine2 = "Test AddressLine2",
            CityId = 0,
            Description = "Test Description"
        };
        Assert.Throws<SqlException>(() => repository.Insert(storage));

    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        StorageRepository repository = new(_connection!);
        Storage? current = repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");
        repository.Delete(Constants.DeleteTestId);
        Storage? deleted = repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        StorageRepository repository = new(_connection!);
        Storage? current = repository.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");
        repository.Delete(Constants.DeleteTestId2);
        Storage? deleted = repository.Get(Constants.DeleteTestId2);
    }
}
