using MyBank.Application.Interfaces.Repositories;
using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services;

public interface ICustomerService
{
    Customer FindCustomer(int customerId);
    Task<Customer> FindCustomerAsync(int customerId, CancellationToken cancellationToken);
    IEnumerable<Account> ListAccountsByCustomer(int customerId);
    IAsyncEnumerable<Account> ListAccountsByCustomerAsync(int customerId, CancellationToken cancellationToken);
    IEnumerable<Customer> ListAllCustomers();
    IAsyncEnumerable<Customer> ListAllCustomersAsync(CancellationToken cancellationToken);
    void RegisterNewCustomer(
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
    void UpdateCustomerDisplayInfo(
    int customerId,
    string firstName,
    string lastName,
    Gender gender,
    DateTime dateOfBirth);
    Task UpdateCustomerDisplayInfoAsync(
    int customerId,
    string firstName,
    string lastName,
    Gender gender,
    DateTime dateOfBirth,
    CancellationToken cancellationToken);
    void UpdateCustomerPrivateInfo(
    int customerId,
    string personalNumber,
    string email,
    string phoneNumber);
    Task UpdateCustomerPrivateInfoAsync(
    int customerId,
    string personalNumber,
    string email,
    string phoneNumber,
    CancellationToken cancellationToken);
    void UpdateCustomerAddressInfo(
    int customerId,
    string addressLine1,
    string? addressLine2,
    string zipCode,
    int cityId);
    Task UpdateCustomerAddressInfoAsync(
    int customerId,
    string addressLine1,
    string? addressLine2,
    string zipCode,
    int cityId,
    CancellationToken cancellationToken);
}