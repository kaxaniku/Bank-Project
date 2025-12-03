//using Microsoft.EntityFrameworkCore;
//using MyBank.Application;
//using MyBank.Application.Interfaces.Services;
//using MyBank.Domain;
//using MyBank.RepositoryTest;

//namespace MyBank.ServiceTest;

//public class CustomerServiceTests : BaseServiceTests
//{
//    private ICustomerService? _service;

//    [SetUp]
//    public void Setup()
//    {
//        EmailSettings settings = new EmailSettings();
//        settings.SmtpServer = "smtp.gmail.com";
//        settings.SmtpPort = 587;
//        settings.Password = "mhwg rwoe bkek svhs";
//        settings.FromAddress = "knindustrybank@gmail.com";
//        _service = new CustomerService(_unitOfWork!, new EmailService(settings));
//    }

//    [Test]
//    public void TestRegister_ShouldRegister()
//    {
//        Customer customer = new()
//        {
//            PersonalNumber = "01703406525",
//            FirstName = "Test",
//            LastName = "User",
//            Gender = Gender.Male,
//            DateOfBirth = new DateTime(1990, 1, 1),
//            Email = "knindustrybank@gmail.com",
//            PhoneNumber = "+1234567890",
//            Address = new AddressInfo
//            {
//                AddressLine1 = "123 Test St",
//                AddressLine2 = "Test City",
//                ZipCode = "1234"
//            },
//            City = _unitOfWork!.CityRepository.GetById(1)!,
//            Activity = new ActivityInfo()
//        };

//        _service!.RegisterNewCustomer(customer);

//        Customer insertedCustomer = _service.FindCustomerById(customer.CustomerId)!;
//        Assert.That(insertedCustomer, Is.Not.Null);
//        Assert.That(customer.PersonalNumber, Is.EqualTo(insertedCustomer.PersonalNumber));
//        Assert.That(customer.FirstName, Is.EqualTo(insertedCustomer.FirstName));
//        Assert.That(customer.LastName, Is.EqualTo(insertedCustomer.LastName));
//        Assert.That(customer.Gender, Is.EqualTo(insertedCustomer.Gender));
//        Assert.That(customer.DateOfBirth, Is.EqualTo(insertedCustomer.DateOfBirth));
//        Assert.That(customer.Email, Is.EqualTo(insertedCustomer.Email));
//        Assert.That(customer.PhoneNumber, Is.EqualTo(insertedCustomer.PhoneNumber));
//        Assert.That(customer.City, Is.EqualTo(insertedCustomer.City));
//        Assert.That(customer.Address, Is.EqualTo(insertedCustomer.Address));
//    }

//    [Test]
//    public void TestRegister_ShouldNotRegister()
//    {
//        Customer customer = new()
//        {
//            PersonalNumber = "145345234256",
//            FirstName = "Test",
//            LastName = "User",
//            Gender = Gender.Male,
//            DateOfBirth = new DateTime(1990, 1, 1),
//            Email = "TestUser@Gmail.com",
//            PhoneNumber = "+1234567890",
//            Address = new AddressInfo
//            {
//                AddressLine1 = "123 Test St",
//                AddressLine2 = "Test City",
//                ZipCode = "1234"
//            },
//            City = _unitOfWork!.CityRepository.GetById(1)!,
//            Activity = new ActivityInfo()
//        };

//        Assert.Throws<DbUpdateException>(() =>
//        {
//            _service!.RegisterNewCustomer(customer);
//        });
//    }

//    [Test]
//    public void TestUpdate_ShouldUpdate()
//    {
//        Customer customer = _service!.FindCustomerById(Constants.UpdateTestID)!;
//        Assert.That(customer, Is.Not.Null);

//        customer.PersonalNumber = "01234567894";
//        customer.FirstName = "UpdatedFirstName";
//        customer.LastName = "UpdatedLastName";
//        customer.Gender = Gender.Female;
//        customer.DateOfBirth = new DateTime(1988, 12, 25);
//        customer.Email = "updateduser@example.com";
//        customer.PhoneNumber = "+1122334455";
//        customer.Address.AddressLine1 = "789 Updated Ave";
//        customer.Address.AddressLine2 = "New City";
//        customer.Address.ZipCode = "9101";
//        customer.City = _unitOfWork!.CityRepository.GetById(3)!;
//        customer.Activity = new ActivityInfo();
//        _service.UpdateCustomer(customer);

//        Customer updatedCustomer = _service!.FindCustomerById(Constants.UpdateTestID)!;
//        Assert.That(updatedCustomer, Is.Not.Null);
//        Assert.That(customer.PersonalNumber, Is.EqualTo(updatedCustomer.PersonalNumber));
//        Assert.That(customer.FirstName, Is.EqualTo(updatedCustomer.FirstName));
//        Assert.That(customer.LastName, Is.EqualTo(updatedCustomer.LastName));
//        Assert.That(customer.Gender, Is.EqualTo(updatedCustomer.Gender));
//        Assert.That(customer.DateOfBirth, Is.EqualTo(updatedCustomer.DateOfBirth));
//        Assert.That(customer.Email, Is.EqualTo(updatedCustomer.Email));
//        Assert.That(customer.PhoneNumber, Is.EqualTo(updatedCustomer.PhoneNumber));
//        Assert.That(customer.Address.AddressLine1, Is.EqualTo(updatedCustomer.Address.AddressLine1));
//        Assert.That(customer.Address.AddressLine2, Is.EqualTo(updatedCustomer.Address.AddressLine2));
//        Assert.That(customer.Address.ZipCode, Is.EqualTo(updatedCustomer.Address.ZipCode));
//        Assert.That(customer.City, Is.EqualTo(updatedCustomer.City));
//    }

//    [Test]
//    public void TestUpdate_ShouldNotUpdate()
//    {
//        Customer customer = _service!.FindCustomerById(Constants.UpdateTestID)!;
//        Assert.That(customer, Is.Not.Null);
//        customer.FirstName = null!;

//        Assert.Throws<DbUpdateException>(() =>
//        {
//            _service.UpdateCustomer(customer);
//        });
//    }

//    [Test]
//    public void TestRemove_ShouldRemove()
//    {
//        _service!.RemoveCustomer(Constants.DeleteTestID);

//        Assert.Throws<InvalidOperationException>(() =>
//        {
//            Customer customer = _service!.FindCustomerById(Constants.DeleteTestID)!;
//        });
//    }

//    [Test]
//    public void ListAllCustomers_ShouldReturnActiveCustomers()
//    {
//        var activeCustomers = _service!.ListAllCustomers();
//        Assert.That(activeCustomers, Is.Not.Null);
//        Assert.That(activeCustomers.All(c => c.Activity.IsActive), Is.True);
//    }

//    [Test]
//    public void ListAccountsByCustomerId_ShouldReturnAccounts()
//    {
//        var accounts = _service!.ListAccountsByCustomer(1);
//        Assert.That(accounts, Is.Not.Null);
//        Assert.That(accounts.All(a => a.Customer != null));
//        Assert.That(accounts.All(a => a.Customer.CustomerId == 1), Is.True);
//    }

//    [Test]
//    public void ListAccountsByCustomerId_ShouldNotReturnAccounts()
//    {
//        var accounts = _service!.ListAccountsByCustomer(0);
//        Assert.That(accounts, Is.Empty);
//    }

//    [Test]
//    public async Task TestRegister_ShouldRegisterAsync()
//    {
//        Customer customer = new()
//        {
//            PersonalNumber = "01703406529",
//            FirstName = "Test",
//            LastName = "User",
//            Gender = Gender.Male,
//            DateOfBirth = new DateTime(1990, 1, 1),
//            Email = "knindustrybank@gmail.com",
//            PhoneNumber = "+1234567890",
//            Address = new AddressInfo
//            {
//                AddressLine1 = "123 Test St",
//                AddressLine2 = "Test City",
//                ZipCode = "1234"
//            },
//            City = _unitOfWork!.CityRepository.GetById(1)!,
//            Activity = new ActivityInfo()
//        };

//        await _service!.RegisterNewCustomerAsync(customer, _cts.Token);

//        Customer? insertedCustomer = await _service.FindCustomerByIdAsync(customer.CustomerId, _cts.Token)!;
//        Assert.That(insertedCustomer, Is.Not.Null);
//        Assert.That(customer.PersonalNumber, Is.EqualTo(insertedCustomer.PersonalNumber));
//        Assert.That(customer.FirstName, Is.EqualTo(insertedCustomer.FirstName));
//        Assert.That(customer.LastName, Is.EqualTo(insertedCustomer.LastName));
//        Assert.That(customer.Gender, Is.EqualTo(insertedCustomer.Gender));
//        Assert.That(customer.DateOfBirth, Is.EqualTo(insertedCustomer.DateOfBirth));
//        Assert.That(customer.Email, Is.EqualTo(insertedCustomer.Email));
//        Assert.That(customer.PhoneNumber, Is.EqualTo(insertedCustomer.PhoneNumber));
//        Assert.That(customer.City, Is.EqualTo(insertedCustomer.City));
//        Assert.That(customer.Address, Is.EqualTo(insertedCustomer.Address));
//    }

//    [Test]
//    public void TestRegister_ShouldNotRegisterAsync()
//    {
//        Customer customer = new()
//        {
//            PersonalNumber = "145345234256",
//            FirstName = "Test",
//            LastName = "User",
//            Gender = Gender.Male,
//            DateOfBirth = new DateTime(1990, 1, 1),
//            Email = "TestUser@Gmail.com",
//            PhoneNumber = "+1234567890",
//            Address = new AddressInfo
//            {
//                AddressLine1 = "123 Test St",
//                AddressLine2 = "Test City",
//                ZipCode = "1234"
//            },
//            City = _unitOfWork!.CityRepository.GetById(1)!,
//            Activity = new ActivityInfo()
//        };

//        Assert.ThrowsAsync<DbUpdateException>(async Task() =>
//        {
//            await _service!.RegisterNewCustomerAsync(customer, _cts.Token);
//        });
//    }

//    [Test]
//    public async Task TestUpdate_ShouldUpdateAsync()
//    {
//        Customer? customer = await _service!.FindCustomerByIdAsync(Constants.UpdateTestID, _cts.Token)!;
//        Assert.That(customer, Is.Not.Null);

//        customer.PersonalNumber = "01234567894";
//        customer.FirstName = "UpdatedFirstName";
//        customer.LastName = "UpdatedLastName";
//        customer.Gender = Gender.Female;
//        customer.DateOfBirth = new DateTime(1988, 12, 25);
//        customer.Email = "updateduser@example.com";
//        customer.PhoneNumber = "+1122334455";
//        customer.Address.AddressLine1 = "789 Updated Ave";
//        customer.Address.AddressLine2 = "New City";
//        customer.Address.ZipCode = "9101";
//        customer.City = _unitOfWork!.CityRepository.GetById(3)!;
//        customer.Activity = new ActivityInfo();
//        await _service.UpdateCustomerAsync(customer, _cts.Token);

//        Customer? updatedCustomer = await _service!.FindCustomerByIdAsync(Constants.UpdateTestID, _cts.Token)!;
//        Assert.That(updatedCustomer, Is.Not.Null);
//        Assert.That(customer.PersonalNumber, Is.EqualTo(updatedCustomer.PersonalNumber));
//        Assert.That(customer.FirstName, Is.EqualTo(updatedCustomer.FirstName));
//        Assert.That(customer.LastName, Is.EqualTo(updatedCustomer.LastName));
//        Assert.That(customer.Gender, Is.EqualTo(updatedCustomer.Gender));
//        Assert.That(customer.DateOfBirth, Is.EqualTo(updatedCustomer.DateOfBirth));
//        Assert.That(customer.Email, Is.EqualTo(updatedCustomer.Email));
//        Assert.That(customer.PhoneNumber, Is.EqualTo(updatedCustomer.PhoneNumber));
//        Assert.That(customer.Address.AddressLine1, Is.EqualTo(updatedCustomer.Address.AddressLine1));
//        Assert.That(customer.Address.AddressLine2, Is.EqualTo(updatedCustomer.Address.AddressLine2));
//        Assert.That(customer.Address.ZipCode, Is.EqualTo(updatedCustomer.Address.ZipCode));
//        Assert.That(customer.City, Is.EqualTo(updatedCustomer.City));
//    }

//    [Test]
//    public async Task TestUpdate_ShouldNotUpdateAsync()
//    {
//        Customer? customer = await _service!.FindCustomerByIdAsync(Constants.UpdateTestID, _cts.Token)!;
//        Assert.That(customer, Is.Not.Null);
//        customer.FirstName = null!;

//        Assert.ThrowsAsync<DbUpdateException>(async Task() =>
//        {
//            await _service.UpdateCustomerAsync(customer, _cts.Token);
//        });
//    }

//    [Test]
//    public async Task TestRemove_ShouldRemoveAsync()
//    {
//        await _service!.RemoveCustomerAsync(Constants.DeleteTestID3, _cts.Token);

//        Assert.ThrowsAsync<InvalidOperationException>(async Task () =>
//        {
//            Customer deletedCustomer = await _service!.FindCustomerByIdAsync(Constants.DeleteTestID, _cts.Token)!;
//        });
//    }

//    [Test]
//    public async Task ListAllCustomers_ShouldReturnActiveCustomersAsync()
//    {
//        var activeCustomers = await _service!.ListAllCustomersAsync(_cts.Token);
//        Assert.That(activeCustomers, Is.Not.Null);
//        Assert.That(activeCustomers.All(c => c.Activity.IsActive), Is.True);
//    }

//    [Test]
//    public async Task ListAccountsByCustomerId_ShouldReturnAccountsAsync()
//    {
//        var accounts = await _service!.ListAccountsByCustomerAsync(1, _cts.Token);
//        Assert.That(accounts, Is.Not.Null);
//        Assert.That(accounts.All(a => a.Customer != null));
//        Assert.That(accounts.All(a => a.Customer.CustomerId == 1), Is.True);
//    }

//    [Test]
//    public async Task ListAccountsByCustomerId_ShouldNotReturnAccountsAsync()
//    {
//        var accounts = await _service!.ListAccountsByCustomerAsync(0, _cts.Token);
//        Assert.That(accounts, Is.Empty);
//    }
//}