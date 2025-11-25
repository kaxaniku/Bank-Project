using MyBank.Application.Interfaces.Repositories;
using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services;

public interface ICustomerService
{
    Customer? FindCustomerById(int customerId);
    Task<Customer?> FindCustomerByIdAsync(int customerId, CancellationToken cancellationToken);
    IEnumerable<Account> ListAccountsByCustomer(int customerId);
    Task<IEnumerable<Account>> ListAccountsByCustomerAsync(int customerId, CancellationToken cancellationToken);
    IEnumerable<Customer> ListAllCustomers();
    Task<IEnumerable<Customer>> ListAllCustomersAsync(CancellationToken cancellationToken);
    void RegisterNewCustomer(Customer customer);
    Task RegisterNewCustomerAsync(Customer customer, CancellationToken cancellationToken);
    void RemoveCustomer(int customerId);
    Task RemoveCustomerAsync(int customerId, CancellationToken cancellationToken);
    void UpdateCustomer(Customer customer);
    Task UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken);
}