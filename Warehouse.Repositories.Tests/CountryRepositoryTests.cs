using Warehouse.DTO;

namespace Warehouse.Repositories.Tests;

public class CountryRepositoryTests : BaseRepositoryTests<Country>
{

    [Test]
    public void TestInsert_ShouldInsert()
    {

        CountryRepository repository = new(_connection!);
        Country country = new()
        {
            Name = "Test Country",
            ISOCode = "Test ISOCode"
        };

        int id = (int)repository.Insert(country);
        Country? result = repository.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(country.Name, result!.Name);
        Assert.AreEqual(country.ISOCode, result!.ISOCode);
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        CountryRepository repository = new(_connection!);
        Country? current = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = "Updated " + current.Name;
        current.ISOCode = "Updated " + current.ISOCode;
        repository.Update(current);

        Country? updated = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.Name, updated!.Name);
        Assert.AreEqual(current.ISOCode, updated!.ISOCode);
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        CountryRepository repository = new(_connection!);
        Country? current = repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        repository.Delete(Constants.DeleteTestId);
        Country? deleted = repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }
}