using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Services.Interfaces.Repositories;

namespace Warehouse.Repositories.Tests;

public class CountryRepositoryTests : BaseRepositoryTests<Country>
{
    private ICountryRepository? _repository;

    [SetUp]
    public void Setup()
    {
        _repository = _unitOfWork!.CountryRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Country country = new()
        {
            Name = "Test Country",
            ISOCode = "123"
        };

        int id = (int)_repository!.Insert(country);
        Country? result = _repository!.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(country.Name, result!.Name);
        Assert.AreEqual(country.ISOCode, result!.ISOCode);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        Country country = new()
        {
            Name = "Test Country",
            ISOCode = null,
        };

        Assert.Throws<SqlException>(() => _repository!.Insert(country));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        Country? current = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = "Updated " + current.Name;
        current.ISOCode = "124";
        _repository!.Update(current);

        Country? updated = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.Name, updated!.Name);
        Assert.AreEqual(current.ISOCode, updated!.ISOCode);
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        Country? current = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = "null";
        current.ISOCode = null;

        Assert.Throws<SqlException>(() => _repository!.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Country? current = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        _repository!.Delete(Constants.DeleteTestId);
        Country? deleted = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Country? current = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        _repository!.Delete(Constants.DeleteTestId2);
        Country? deleted = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => _repository!.Delete(Constants.DeleteTestId2));
    }

    [Test]
    public void TestQuery()
    {
        IEnumerable<Country> countries = _repository!.Query(c => c.IsActive && c.CountryId > 2);

        Assert.IsNotNull(countries);
        Assert.IsNotEmpty(countries);

        foreach (var country in countries)
        {
            Assert.IsTrue(country.IsActive, $"Country {country.Name} is not active.");
            Assert.Greater(country.CountryId, 2, $"Country {country.Name} does not have a CountryId greater than 2.");
        }
    }

}