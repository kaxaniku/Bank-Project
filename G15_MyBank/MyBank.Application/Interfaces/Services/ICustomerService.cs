using MyBank.Application.Interfaces.Repositories;
using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services;

public interface ICustomerService
{
    Customer FindCustomer(int customerId);
    Task<Customer> FindCustomerAsync(int customerId, CancellationToken cancellationToken);
    IEnumerable<Account> ListAccountsByCustomer(int customerId);
    Task<IEnumerable<Account>> ListAccountsByCustomerAsync(int customerId, CancellationToken cancellationToken);
    IEnumerable<Customer> ListAllCustomers();
    Task<IEnumerable<Customer>> ListAllCustomersAsync(CancellationToken cancellationToken);
    void RegisterNewCustomer(string personalNumber,
    string firstName,
    string lastName,
    Gender gender,
    string email,
    string phoneNumber,
    DateTime dateOfBirth,
    string addressLine1,
    string? addressLine2,
    string zipCode,
    int cityId);
    Task RegisterNewCustomerAsync(string personalNumber,
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
    CancellationToken cancellationToken);
    void RemoveCustomer(int customerId);
    Task RemoveCustomerAsync(int customerId, CancellationToken cancellationToken);
    void UpdateCustomer(Customer customer);
    Task UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken);
}