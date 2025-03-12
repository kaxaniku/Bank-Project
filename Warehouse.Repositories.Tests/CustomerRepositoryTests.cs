using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories.Tests;

public class CustomerRepositoryTests : BaseRepositoryTests<Customer>
{
    private ICustomerRepository? _repository;

    [SetUp]
    public void Setup()
    {
        _repository = _unitOfWork!.CustomerRepository;
    }

    [Test]

    public void TestInsert_ShouldInsert()
    {
        Customer customer = new()
        {
            Name = "Test Customer",
            AddressLine1 = "Test AddressLine1",
            AddressLine2 = "Test AddressLine2",
            CityId = 1,
            Phone = "Test Phone",
            Email = "test@customer.com",
        };

        int id = (int)_repository!.Insert(customer);
        Customer? result = _repository!.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(customer.Name, result!.Name);
        Assert.AreEqual(customer.AddressLine1, result!.AddressLine1);
        Assert.AreEqual(customer.AddressLine2, result!.AddressLine2);
        Assert.AreEqual(customer.CityId, result!.CityId);
        Assert.AreEqual(customer.Phone, result!.Phone);
        Assert.AreEqual(customer.Email, result!.Email);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        Customer customer = new()
        {
            Name = "Test Customer",
            AddressLine1 = "Test AddressLine1",
            AddressLine2 = "Test AddressLine2",
            Phone = "Test Phone",
            Email = null
        };

        Assert.Throws<SqlException>(() => _repository!.Insert(customer));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        Customer? current = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = "Updated " + current.Name;
        current!.AddressLine1 = "Updated " + current.AddressLine1;
        current.AddressLine2 = "Updated " + current.AddressLine2;
        current.Phone = "Updated " + current.Phone;
        _repository!.Update(current);

        Customer? updated = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.Name, updated!.Name);
        Assert.AreEqual(current.AddressLine1, updated!.AddressLine1);
        Assert.AreEqual(current.AddressLine2, updated.AddressLine2);
        Assert.AreEqual(current.Phone, updated!.Phone);
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        Customer? current = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = null!;
        current!.Phone = "Updated " + current.Phone;
        Assert.Throws<SqlException>(() => _repository!.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Customer? current = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        _repository!.Delete(Constants.DeleteTestId);
        Customer? deleted = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retrieved");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Customer? current = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        _repository!.Delete(Constants.DeleteTestId2);
        Customer? deleted = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retrieved");

        Assert.Throws<SqlException>(() => _repository!.Delete(Constants.DeleteTestId2));
    }
}
