using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories.Tests;

public class StorageRepositoryTests : BaseRepositoryTests<Tag>
{
    private IStorageRepository? _repository;

    [SetUp]
    public void Setup()
    {
        _repository = _unitOfWork!.StorageRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Storage storage = new()
        {
            Name = "Test Storage",
            AddressLine1 = "Test AddressLine1",
            AddressLine2 = "Test AddressLine2",
            CityId = 1,
            Description = "Test Description"

        };
        int id = (int)_repository!.Insert(storage);
        Storage? result = _repository!.Get(id);
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
        Storage storage = new()
        {
            Name = null,
            AddressLine1 = "Test AddressLine1",
            AddressLine2 = "Test AddressLine2",
            CityId = 0,
            Description = "Test Description"
        };
        Assert.Throws<SqlException>(() => _repository!.Insert(storage));

    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Storage? current = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");
        _repository!.Delete(Constants.DeleteTestId);
        Storage? deleted = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Storage? current = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");
        _repository!.Delete(Constants.DeleteTestId2);
        Storage? deleted = _repository!.Get(Constants.DeleteTestId2);
    }
}
