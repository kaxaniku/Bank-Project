using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Application.Interfaces.Repositories;

namespace MyBank.RepositoryTest;

public class CustomerRepositoryTests : BaseRepositoryTests<Customer>
{
    private ICustomerRepository? _repository;

    public override void SetUp()
    {
        base.SetUp();
        _repository = _unitOfWork!.CustomerRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Customer customer = new()
        {
            PersonalNumber = "01703406525",
            FirstName = "Test",
            LastName = "User",
            Gender = Gender.Male,
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = "TestUser@Gmail.com",
            PhoneNumber = "+1234567890",
            Address = new AddressInfo
            {
                AddressLine1 = "123 Test St",
                AddressLine2 = "Test City",
                ZipCode = "1234"
            },
            City = _unitOfWork!.CityRepository.GetById(1)!,
            Activity = new ActivityInfo()
        };

        _repository!.Insert(customer);
        _unitOfWork!.SaveChanges();

        Customer insertedCustomer = _repository.GetById(customer.CustomerId)!;
        Assert.IsNotNull(insertedCustomer);
        Assert.That(customer.PersonalNumber, Is.EqualTo(insertedCustomer.PersonalNumber));
        Assert.That(customer.FirstName, Is.EqualTo(insertedCustomer.FirstName));
        Assert.That(customer.LastName, Is.EqualTo(insertedCustomer.LastName));
        Assert.That(customer.Gender, Is.EqualTo(insertedCustomer.Gender));
        Assert.That(customer.DateOfBirth, Is.EqualTo(insertedCustomer.DateOfBirth));
        Assert.That(customer.Email, Is.EqualTo(insertedCustomer.Email));
        Assert.That(customer.PhoneNumber, Is.EqualTo(insertedCustomer.PhoneNumber));
        Assert.That(customer.City, Is.EqualTo(insertedCustomer.City));
        Assert.That(customer.Address, Is.EqualTo(insertedCustomer.Address));
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        Customer customer = new()
        {
            PersonalNumber = "145345234256",
            FirstName = "Test",
            LastName = "User",
            Gender = Gender.Male,
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = "TestUser@Gmail.com",
            PhoneNumber = "+1234567890",
            Address = new AddressInfo
            {
                AddressLine1 = "123 Test St",
                AddressLine2 = "Test City",
                ZipCode = "1234"
            },
            City = _unitOfWork!.CityRepository.GetById(1)!,
            Activity = new ActivityInfo()
        };

        Assert.Throws<DbUpdateException>(() =>
        {
            _repository!.Insert(customer);
            _unitOfWork!.SaveChanges();
        });
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        Customer customer = _repository!.GetById(Constants.UpdateTestID)!;
        Assert.IsNotNull(customer);

        customer.PersonalNumber = "01234567894";
        customer.FirstName = "UpdatedFirstName";
        customer.LastName = "UpdatedLastName";
        customer.Gender = Gender.Female;
        customer.DateOfBirth = new DateTime(1988, 12, 25);
        customer.Email = "updateduser@example.com";
        customer.PhoneNumber = "+1122334455";
        customer.Address.AddressLine1 = "789 Updated Ave";
        customer.Address.AddressLine2 = "New City";
        customer.Address.ZipCode = "9101";
        customer.City = _unitOfWork!.CityRepository.GetById(3)!;
        customer.Activity = new ActivityInfo();
        _repository.Update(customer);
        _unitOfWork!.SaveChanges();

        Customer updatedCustomer = _repository!.GetById(Constants.UpdateTestID)!;
        Assert.IsNotNull(updatedCustomer);
        Assert.That(customer.PersonalNumber, Is.EqualTo(updatedCustomer.PersonalNumber));
        Assert.That(customer.FirstName, Is.EqualTo(updatedCustomer.FirstName));
        Assert.That(customer.LastName, Is.EqualTo(updatedCustomer.LastName));
        Assert.That(customer.Gender, Is.EqualTo(updatedCustomer.Gender));
        Assert.That(customer.DateOfBirth, Is.EqualTo(updatedCustomer.DateOfBirth));
        Assert.That(customer.Email, Is.EqualTo(updatedCustomer.Email));
        Assert.That(customer.PhoneNumber, Is.EqualTo(updatedCustomer.PhoneNumber));
        Assert.That(customer.Address.AddressLine1, Is.EqualTo(updatedCustomer.Address.AddressLine1));
        Assert.That(customer.Address.AddressLine2, Is.EqualTo(updatedCustomer.Address.AddressLine2));
        Assert.That(customer.Address.ZipCode, Is.EqualTo(updatedCustomer.Address.ZipCode));
        Assert.That(customer.City, Is.EqualTo(updatedCustomer.City));
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        Customer customer = _repository!.GetById(Constants.UpdateTestID)!;
        Assert.IsNotNull(customer);
        customer.FirstName = null!;

        Assert.Throws<DbUpdateException>(() =>
        {
            _repository.Update(customer);
            _unitOfWork!.SaveChanges();
        });
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Customer customer = _repository!.GetById(Constants.DeleteTestID)!;
        Assert.IsNotNull(customer, $"Record with Id {Constants.DeleteTestID} doesn't exist");

        _repository.Delete(customer);
        _unitOfWork!.SaveChanges();

        Customer deletedCustomer = _repository!.GetById(Constants.DeleteTestID)!;
        Assert.That(deletedCustomer.Activity.IsActive, Is.EqualTo(false));
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Customer customer = _repository!.GetById(Constants.DeleteTestID2)!;
        Assert.IsNotNull(customer, $"Record with Id {Constants.DeleteTestID2} doesn't exist");

        _repository.Delete(customer);
        _unitOfWork!.SaveChanges();

        Customer deletedCustomer = _repository!.GetById(Constants.DeleteTestID2)!;
        Assert.That(deletedCustomer.Activity.IsActive, Is.EqualTo(false));
        Assert.Throws<DbUpdateConcurrencyException>(() =>
        {
            _repository.Delete(customer);
            _unitOfWork!.SaveChanges();
        });
    }
}