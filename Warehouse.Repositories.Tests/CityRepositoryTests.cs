using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories.Tests;

public class CityRepositoryTests : BaseRepositoryTests<City>
{
    private ICityRepository? _repository;

    [SetUp]
    public void Setup()
    {
        _repository = _unitOfWork!.CityRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {

        City city = new()
        {
            Name = "Test City",
            PostCode = "Test PostCode",
            CountryId = 1
        };

        int id = (int)_repository!.Insert(city);
        City? result = _repository!.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(city.Name, result!.Name);
        Assert.AreEqual(city.PostCode, result!.PostCode);
        Assert.AreEqual(city.CountryId, result!.CountryId);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        City city = new()
        {
            Name = null,
            PostCode = "Test PostCode",
            CountryId = 1
        };

        Assert.Throws<SqlException>(() => _repository!.Insert(city));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        City? current = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = "Updated " + current.Name;
        current.PostCode = "Updated " + current.PostCode;
        current.CountryId = 2;
        _repository!.Update(current);

        City? updated = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.Name, updated!.Name);
        Assert.AreEqual(current.PostCode, updated!.PostCode);
        Assert.AreEqual(current.CountryId, updated!.CountryId);
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        City? current = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = null;
        current.PostCode = "Updated " + current.PostCode;
        current.CountryId = 2;

        Assert.Throws<SqlException>(() => _repository!.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        City? current = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        _repository!.Delete(Constants.DeleteTestId);
        City? deleted = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        City? current = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        _repository!.Delete(Constants.DeleteTestId2);
        City? deleted = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => _repository!.Delete(Constants.DeleteTestId2));
    }
}