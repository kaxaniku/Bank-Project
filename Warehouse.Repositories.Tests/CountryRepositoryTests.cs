using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories.Tests;

public class CountryRepositoryTests : BaseRepositoryTests<Country>
{
    public ICountryRepository Repository => new UnitOfWork(_connection!).CountryRepository;

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Country country = new()
        {
            Name = "Test Country",
            ISOCode = "123"
        };

        int id = (int)Repository.Insert(country);
        Country? result = Repository.Get(id);

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

        Assert.Throws<SqlException>(() => Repository.Insert(country));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        Country? current = Repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = "Updated " + current.Name;
        current.ISOCode = "124";
        Repository.Update(current);

        Country? updated = Repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.Name, updated!.Name);
        Assert.AreEqual(current.ISOCode, updated!.ISOCode);
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        Country? current = Repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = "null";
        current.ISOCode = null;

        Assert.Throws<SqlException>(() => Repository.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Country? current = Repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        Repository.Delete(Constants.DeleteTestId);
        Country? deleted = Repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Country? current = Repository.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        Repository.Delete(Constants.DeleteTestId2);
        Country? deleted = Repository.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => Repository.Delete(Constants.DeleteTestId2));
    }
}