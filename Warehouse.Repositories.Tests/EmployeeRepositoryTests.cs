using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories.Tests;

public class EmployeeyRepositoryTests : BaseRepositoryTests<Employee>
{
    public IEmployeeRepository Repository => new UnitOfWork(_connection!).EmployeeRepository;

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Employee employee = new()
        {
            FirstName = "Test FirstName",
            LastName = "Test LastName",
            Email = "Test@Email.com",
            AddressLine1 = "Test AddressLine1",
            AddressLine2 = "Test AddressLine2",
            CityId = 1,
            PhoneNumber = "Test PhoneNumber",
            BirthDate = DateTime.Today.AddYears(-30),
            HireDate = DateTime.Today.AddMonths(-8),
            PositionId = 1,
            ReportsTo = 1,
        };

        int id = (int)Repository.Insert(employee);
        Employee? result = Repository.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(employee.FirstName, result!.FirstName);
        Assert.AreEqual(employee.LastName, result!.LastName);
        Assert.AreEqual(employee.Email, result!.Email);
        Assert.AreEqual(employee.AddressLine1, result!.AddressLine1);
        Assert.AreEqual(employee.AddressLine2, result!.AddressLine2);
        Assert.AreEqual(employee.CityId, result!.CityId);
        Assert.AreEqual(employee.PhoneNumber, result!.PhoneNumber);
        Assert.AreEqual(employee.BirthDate, result!.BirthDate);
        Assert.AreEqual(employee.HireDate, result!.HireDate);
        Assert.AreEqual(employee.PositionId, result!.PositionId);
        Assert.AreEqual(employee.ReportsTo, result!.ReportsTo);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        Employee employee = new()
        {
            FirstName = "Test FirstName",
            LastName = "Test LastName",
            Email = null,
            AddressLine1 = "Test AddressLine1",
            AddressLine2 = "Test AddressLine2",
            EmployeeId = 1,
            PhoneNumber = "Test PhoneNumber",
            BirthDate = DateTime.Today.AddYears(-30),
            HireDate = DateTime.Today.AddMonths(-8),
            PositionId = 1,
            ReportsTo = 1,
        };

        Assert.Throws<SqlException>(() => Repository.Insert(employee));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        Employee? current = Repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.FirstName = "Updated " + current.FirstName;
        current!.LastName = "Updated " + current.LastName;
        current!.Email = "New" + current.Email;
        current!.AddressLine1 = "Updated " + current.AddressLine1;
        current.AddressLine2 = "Updated " + current.AddressLine2;
        current.PhoneNumber = "Updated " + current.PhoneNumber;
        current.BirthDate = current.BirthDate.AddYears(-15);
        current.HireDate = current.HireDate.AddMonths(-4);
        Repository.Update(current);

        Employee? updated = Repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.FirstName, updated!.FirstName);
        Assert.AreEqual(current.LastName, updated!.LastName);
        Assert.AreEqual(current.Email, updated!.Email);
        Assert.AreEqual(current.AddressLine1, updated!.AddressLine1);
        Assert.AreEqual(current.AddressLine2, updated.AddressLine2);
        Assert.AreEqual(current.BirthDate, updated!.BirthDate);
        Assert.AreEqual(current.HireDate, updated!.HireDate);
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        Employee? current = Repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.FirstName = "Updated " + current.FirstName;
        current!.LastName = null;
        current!.Email = "New" + current.Email;
        current!.AddressLine1 = "Updated " + current.AddressLine1;
        current.AddressLine2 = "Updated " + current.AddressLine2;
        current.PhoneNumber = "Updated " + current.PhoneNumber;
        current.BirthDate = current.BirthDate.AddYears(-15);
        current.HireDate = current.HireDate.AddMonths(-4);

        Assert.Throws<SqlException>(() => Repository.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Employee? current = Repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        Repository.Delete(Constants.DeleteTestId);
        Employee? deleted = Repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Employee? current = Repository.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        Repository.Delete(Constants.DeleteTestId2);
        Employee? deleted = Repository.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => Repository.Delete(Constants.DeleteTestId2));
    }
}