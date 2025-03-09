using Microsoft.Data.SqlClient;
using Warehouse.DTO;

namespace Warehouse.Repositories.Tests;

public class CityRepositoryTests : BaseRepositoryTests<City>
{

    [Test]
    public void TestInsert_ShouldInsert()
    {

        CityRepository repository = new(_connection!);
        City city = new()
        {
            Name = "Test City",
            PostCode = "Test PostCode",
            CountryId = 1
        };

        int id = (int)repository.Insert(city);
        City? result = repository.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(city.Name, result!.Name);
        Assert.AreEqual(city.PostCode, result!.PostCode);
        Assert.AreEqual(city.CountryId, result!.CountryId);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        CityRepository repository = new(_connection!);
        City city = new()
        {
            Name = null,
            PostCode = "Test PostCode",
            CountryId = 1
        };

        Assert.Throws<SqlException>(() => repository.Insert(city));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        CityRepository repository = new(_connection!);
        City? current = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = "Updated " + current.Name;
        current.PostCode = "Updated " + current.PostCode;
        current.CountryId = 2;
        repository.Update(current);

        City? updated = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.Name, updated!.Name);
        Assert.AreEqual(current.PostCode, updated!.PostCode);
        Assert.AreEqual(current.CountryId, updated!.CountryId);
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        CityRepository repository = new(_connection!);
        City? current = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = null;
        current.PostCode = "Updated " + current.PostCode;
        current.CountryId = 2;

        Assert.Throws<SqlException>(() => repository.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        CityRepository repository = new(_connection!);
        City? current = repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        repository.Delete(Constants.DeleteTestId);
        City? deleted = repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        CityRepository repository = new(_connection!);
        City? current = repository.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        repository.Delete(Constants.DeleteTestId2);
        City? deleted = repository.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => repository.Delete(Constants.DeleteTestId2));
    }
}