using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Services.Interfaces.Repositories;

namespace Warehouse.Repositories.Tests;

public class SlotRepositoryTests : BaseRepositoryTests<Slot>
{
    private ISlotRepository? _repository;

    [SetUp]
    public void Setup()
    {
        _repository = _unitOfWork!.SlotRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Slot slot = new()
        {
            StorageId = 1,
            SlotCode = "D1"
        };

        int id = (int)_repository!.Insert(slot);
        Slot? result = _repository!.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(slot.StorageId, result!.StorageId);
        Assert.AreEqual(slot.SlotCode, result!.SlotCode);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        Slot slot = new()
        {
            StorageId = 0, // Invalid ID, should fail
            SlotCode = "B2"
        };

        Assert.Throws<SqlException>(() => _repository!.Insert(slot));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Slot? current = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        _repository!.Delete(Constants.DeleteTestId);
        Slot? deleted = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Slot? current = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        _repository!.Delete(Constants.DeleteTestId2);
        Slot? deleted = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => _repository!.Delete(Constants.DeleteTestId2));
    }

    [Test]
    public void TestQuery()
    {
        IEnumerable<Slot> slots = _repository!.Query(slot => slot.StorageId == 1);

        Assert.IsNotNull(slots);
        Assert.IsNotEmpty(slots);

        foreach (var slot in slots)
        {
            Assert.AreEqual(1, slot.StorageId, $"Slot with SlotCode {slot.SlotCode} does not have the expected StorageId.");
        }
    }

}
