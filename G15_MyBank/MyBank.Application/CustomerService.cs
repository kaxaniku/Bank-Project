using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.Application;

public sealed class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly ICountryCityServices _countryCityService;

    public static event Action<Customer>? CustomerRegistered;
    public static event Action<Customer>? CustomerUpdated;
    public static event Action<int>? CustomerRemoved;

    public CustomerService(IUnitOfWork unitOfWork, IEmailService emailService, ICountryCityServices countryCity)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _countryCityService = countryCity ?? throw new ArgumentNullException(nameof(countryCity));
    }

    public void RegisterNewCustomer(
        string personalNumber,
        string firstName,
        string lastName,
        Gender gender,
        string email,
        string phoneNumber,
        DateTime dateOfBirth,
        string addressLine1,
        string? addressLine2,
        string zipCode,
        int cityId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(personalNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(phoneNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine1);
        ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);
        if (dateOfBirth >= DateTime.UtcNow) throw new ArgumentException("Date of birth must be in the past.", nameof(dateOfBirth));

        Customer customer = new Customer
        {
            PersonalNumber = personalNumber,
            FirstName = firstName,
            LastName = lastName,
            Gender = gender,
            Email = email,
            PhoneNumber = phoneNumber,
            DateOfBirth = dateOfBirth,
            Address = new AddressInfo
            {
                AddressLine1 = addressLine1,
                AddressLine2 = addressLine2,
                ZipCode = zipCode
            },
            City = _countryCityService.GetCity(cityId)!,
            Activity = new ActivityInfo()
        };

        _unitOfWork.CustomerRepository.Insert(customer);
        _unitOfWork.SaveChanges();
        _emailService.SendEmail(customer.Email, "Welcome to MyBank", "Thank you for registering with MyBank.");
        OnCustomerRegistered(customer);
    }

    public void UpdateCustomerDisplayInfo(
        int customerId,
        string firstName,
        string lastName,
        Gender gender,
        DateTime dateOfBirth)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        if (dateOfBirth >= DateTime.UtcNow) throw new ArgumentException("Date of birth must be in the past.", nameof(dateOfBirth));

        Customer customer = FindCustomer(customerId);
        customer.FirstName = firstName;
        customer.LastName = lastName;
        customer.Gender = gender;
        customer.DateOfBirth = dateOfBirth;

        _unitOfWork.CustomerRepository.Update(customer);
        _unitOfWork.SaveChanges();
        OnCustomerUpdated(customer);
    }

    public void UpdateCustomerPrivateInfo(
        int customerId,
        string personalNumber,
        string email,
        string phoneNumber)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(personalNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(phoneNumber);

        Customer customer = FindCustomer(customerId);
        customer.PersonalNumber = personalNumber;
        customer.Email = email;
        customer.PhoneNumber = phoneNumber;

        _unitOfWork.CustomerRepository.Update(customer);
        _unitOfWork.SaveChanges();
        OnCustomerUpdated(customer);
    }

    public void UpdateCustomerAddressInfo(
        int customerId,
        string addressLine1,
        string? addressLine2,
        string zipCode,
        int cityId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine1);
        ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);

        Customer customer = FindCustomer(customerId);
        customer.Address.AddressLine1 = addressLine1;
        customer.Address.AddressLine2 = addressLine2;
        customer.Address.ZipCode = zipCode;
        customer.City = _countryCityService.GetCity(cityId)!;

        _unitOfWork.CustomerRepository.Update(customer);
        _unitOfWork.SaveChanges();
        OnCustomerUpdated(customer);
    }

    public void RemoveCustomer(int customerId)
    {
        Customer customer = FindCustomer(customerId);

        _unitOfWork.CustomerRepository.Delete(customer);
        _unitOfWork.SaveChanges();
        OnCustomerRemoved(customerId);
    }

    public Customer FindCustomer(int customerId)
    {
        Customer customer = _unitOfWork.CustomerRepository.GetById(customerId)
            ?? throw new InvalidOperationException($"Customer with ID {customerId} does not exist.");
        if (!customer.Activity.IsActive)
            throw new InvalidOperationException($"Customer with ID {customerId} no longer exists.");
        return customer;
    }

    // TODO: Add pagination support
    public IEnumerable<Customer> ListAllCustomers()
    {
        return _unitOfWork.CustomerRepository.Query(x => x.Activity.IsActive);
    }

    public IEnumerable<Account> ListAccountsByCustomer(int customerId)
    {
        return _unitOfWork.AccountRepository.Query(x => x.Customer.CustomerId == customerId, x => x.Customer);
    }

    public async Task RegisterNewCustomerAsync(
        string personalNumber,
        string firstName,
        string lastName,
        Gender gender,
        string email,
        string phoneNumber,
        DateTime dateOfBirth,
        string addressLine1,
        string? addressLine2,
        string zipCode,
        int cityId, 
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(personalNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(phoneNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine1);
        ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);
        if (dateOfBirth >= DateTime.UtcNow)
            throw new ArgumentException("Date of birth must be in the past.", nameof(dateOfBirth));

        Customer customer = new Customer
        {
            PersonalNumber = personalNumber,
            FirstName = firstName,
            LastName = lastName,
            Gender = gender,
            Email = email,
            PhoneNumber = phoneNumber,
            DateOfBirth = dateOfBirth,
            Address = new AddressInfo
            {
                AddressLine1 = addressLine1,
                AddressLine2 = addressLine2,
                ZipCode = zipCode
            },
            City = new City { CityId = cityId },
            Activity = new ActivityInfo()
        };

        await _unitOfWork.CustomerRepository.InsertAsync(customer, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _emailService.SendEmailAsync(customer.Email, "Welcome to MyBank", "Thank you for registering with MyBank.");
        OnCustomerRegistered(customer);
    }

    public async Task UpdateCustomerDisplayInfoAsync(
        int customerId,
        string firstName,
        string lastName,
        Gender gender,
        DateTime dateOfBirth,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        if (dateOfBirth >= DateTime.UtcNow)
            throw new ArgumentException("Date of birth must be in the past.", nameof(dateOfBirth));

        Customer customer = await FindCustomerAsync(customerId, cancellationToken);
        customer.FirstName = firstName;
        customer.LastName = lastName;
        customer.Gender = gender;
        customer.DateOfBirth = dateOfBirth;

        await _unitOfWork.CustomerRepository.UpdateAsync(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCustomerUpdated(customer);
    }

    public async Task UpdateCustomerPrivateInfoAsync(
        int customerId,
        string personalNumber,
        string email,
        string phoneNumber,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(personalNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(phoneNumber);

        Customer customer = await FindCustomerAsync(customerId, cancellationToken);
        customer.PersonalNumber = personalNumber;
        customer.Email = email;
        customer.PhoneNumber = phoneNumber;

        await _unitOfWork.CustomerRepository.UpdateAsync(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCustomerUpdated(customer);
    }

    public async Task UpdateCustomerAddressInfoAsync(
        int customerId,
        string addressLine1,
        string? addressLine2,
        string zipCode,
        int cityId,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine1);
        ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);

        Customer customer = await FindCustomerAsync(customerId, cancellationToken);
        customer.Address.AddressLine1 = addressLine1;
        customer.Address.AddressLine2 = addressLine2;
        customer.Address.ZipCode = zipCode;
        customer.City = await _countryCityService.GetCityAsync(cityId, cancellationToken);

        await _unitOfWork.CustomerRepository.UpdateAsync(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCustomerUpdated(customer);
    }

    public async Task RemoveCustomerAsync(int customerId, CancellationToken cancellationToken)
    {
        Customer customer = await FindCustomerAsync(customerId, cancellationToken);
        await _unitOfWork.CustomerRepository.DeleteAsync(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCustomerRemoved(customerId);
    }

    public async Task<Customer> FindCustomerAsync(int customerId, CancellationToken cancellationToken)
    {
        Customer customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId, cancellationToken)
            ?? throw new InvalidOperationException($"Customer with ID {customerId} does not exist.");
        if (!customer.Activity.IsActive)
            throw new InvalidOperationException($"Customer with ID {customerId} no longer exists.");
        return customer;
    }

    // TODO: Add pagination support
    public IAsyncEnumerable<Customer> ListAllCustomersAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.CustomerRepository.QueryAsync(x => x.Activity.IsActive, cancellationToken);
    }

    public IAsyncEnumerable<Account> ListAccountsByCustomerAsync(int customerId, CancellationToken cancellationToken)
    {
        return _unitOfWork.AccountRepository.QueryAsync(x => x.Customer.CustomerId.Equals(customerId), cancellationToken, x => x.Customer);
    }

    private static void OnCustomerRegistered(Customer customer)
    {
        CustomerRegistered?.Invoke(customer);
    }

    private static void OnCustomerUpdated(Customer customer)
    {
        CustomerUpdated?.Invoke(customer);
    }

    private static void OnCustomerRemoved(int customerId)
    {
        CustomerRemoved?.Invoke(customerId);
    }
}
