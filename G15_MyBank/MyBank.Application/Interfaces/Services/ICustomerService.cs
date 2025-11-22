using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services;

public interface ICustomerService
{
    Customer? FindCustomerById(int customerId);
    Task<Customer?> FindCustomerByIdAsync(int customerId);
    IEnumerable<int> ListAccountsByCustomer(int customerId);
    Task<IEnumerable<int>> ListAccountsByCustomerAsync(int customerId);
    IEnumerable<Customer> ListAllCustomers();
    Task<IEnumerable<Customer>> ListAllCustomersAsync();
    void RegisterNewCustomer(Customer customer);
    Task RegisterNewCustomerAsync(Customer customer);
    void RemoveCustomer(int customerId);
    Task RemoveCustomerAsync(int customerId);
    void UpdateCustomer(Customer customer);
    Task UpdateCustomerAsync(Customer customer);
}