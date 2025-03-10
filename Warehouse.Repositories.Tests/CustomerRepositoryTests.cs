using Microsoft.Data.SqlClient;
using Warehouse.DTO;

namespace Warehouse.Repositories.Tests;

public class CustomerRepositoryTests : BaseRepositoryTests<Customer>
{
    [Test]
    public void TestInsert_ShouldInsert()
    {
        CustomerRepository repository = new(_connection!);
        Customer customer = new()
        {
            Name = "Test Customer",
            AddressLine1 = "Test AddressLine1",
            AddressLine2 = "Test AddressLine2",
            CityId = 1,
            Phone = "Test Phone",
            Email = "test@customer.com",
        };

        int id = (int)repository.Insert(customer);
        Customer? result = repository.Get(id);

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
        CustomerRepository repository = new(_connection!);
        Customer customer = new()
        {
            Name = "Test Customer",
            AddressLine1 = "Test AddressLine1",
            AddressLine2 = "Test AddressLine2",
            Phone = "Test Phone",
            Email = null
        };

        Assert.Throws<SqlException>(() => repository.Insert(customer));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        CustomerRepository repository = new(_connection!);
        Customer? current = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = "Updated " + current.Name;
        current!.AddressLine1 = "Updated " + current.AddressLine1;
        current.AddressLine2 = "Updated " + current.AddressLine2;
        current.Phone = "Updated " + current.Phone;
        repository.Update(current);

        Customer? updated = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.Name, updated!.Name);
        Assert.AreEqual(current.AddressLine1, updated!.AddressLine1);
        Assert.AreEqual(current.AddressLine2, updated.AddressLine2);
        Assert.AreEqual(current.Phone, updated!.Phone);
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        CustomerRepository repository = new(_connection!);
        Customer? current = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = null!;
        current!.Phone = "Updated " + current.Phone;
        Assert.Throws<SqlException>(() => repository.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        CustomerRepository repository = new(_connection!);
        Customer? current = repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        repository.Delete(Constants.DeleteTestId);
        Customer? deleted = repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retrieved");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        CustomerRepository repository = new(_connection!);
        Customer? current = repository.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        repository.Delete(Constants.DeleteTestId2);
        Customer? deleted = repository.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retrieved");

        Assert.Throws<SqlException>(() => repository.Delete(Constants.DeleteTestId2));
    }
}
