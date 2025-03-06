using Warehouse.DTO;

namespace Warehouse.Repositories.Tests;

public class EmployeeyRepositoryTests : BaseRepositoryTests<Employee>
{

    [Test]
    public void TestInsert_ShouldInsert()
    {

        EmployeeRepository repository = new(_connection!);
        Employee employee = new()
        {
            FirstName = "Test FirstName",
            LastName = "Test LastName",
            Email = "Test Email",
            AddressLine1 = "Test AddressLine1",
            AddressLine2 = "Test AddressLine2",
            CityId = 1,
            PhoneNumber = "Test PhoneNumber",
            BirthDate = DateTime.Now,
            HireDate = DateTime.Now,
            PositionId = 1,
            ReportsTo = 1,
        };

        int id = (int)repository.Insert(employee);
        Employee? result = repository.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(employee.FirstName, result!.FirstName);
        Assert.AreEqual(employee.LastName, result!.LastName);
        Assert.AreEqual(employee.Email, result!.Email);
        Assert.AreEqual(employee.AddressLine1, result!.AddressLine1);
        Assert.AreEqual(employee.AddressLine2, result!.AddressLine2);
        Assert.AreEqual(employee.CityId, result!.CityId);
        Assert.AreEqual(employee.PhoneNumber, result!.PhoneNumber);
        Assert.AreEqual(employee.BirthDate.ToString("yyyy-MM-dd HH:mm:ss"), result!.BirthDate.ToString("yyyy-MM-dd HH:mm:ss"));
        Assert.AreEqual(employee.HireDate.ToString("yyyy-MM-dd HH:mm:ss"), result!.HireDate.ToString("yyyy-MM-dd HH:mm:ss"));
        Assert.AreEqual(employee.PositionId, result!.PositionId);
        Assert.AreEqual(employee.ReportsTo, result!.ReportsTo);
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        EmployeeRepository repository = new(_connection!);
        Employee? current = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.FirstName = "Updated " + current.FirstName;
        current!.LastName = "Updated " + current.LastName;
        current!.Email = "Updated " + current.Email;
        current!.AddressLine1 = "Updated " + current.AddressLine1;
        current.AddressLine2 = "Updated " + current.AddressLine2;
        current.PhoneNumber = "Updated " + current.PhoneNumber;
        current.BirthDate = DateTime.Now;
        current.HireDate = DateTime.Now;
        repository.Update(current);

        Employee? updated = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.FirstName, updated!.FirstName);
        Assert.AreEqual(current.LastName, updated!.LastName);
        Assert.AreEqual(current.Email, updated!.Email);
        Assert.AreEqual(current.AddressLine1, updated!.AddressLine1);
        Assert.AreEqual(current.AddressLine2, updated.AddressLine2);
        Assert.AreEqual(current.BirthDate.ToString("yyyy-MM-dd HH:mm:ss"), updated!.BirthDate.ToString("yyyy-MM-dd HH:mm:ss"));
        Assert.AreEqual(current.HireDate.ToString("yyyy-MM-dd HH:mm:ss"), updated!.HireDate.ToString("yyyy-MM-dd HH:mm:ss"));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        EmployeeRepository repository = new(_connection!);
        Employee? current = repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        repository.Delete(Constants.DeleteTestId);
        Employee? deleted = repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }
}