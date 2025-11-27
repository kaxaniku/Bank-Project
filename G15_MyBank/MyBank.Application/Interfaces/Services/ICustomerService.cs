using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services
{
    internal interface ICustomerService 
    {
        Customer? FindCustomerById(int customerId);
        Task<Customer?> FindCustomerByIdAsync(int customerId, CancellationToken token);
        IEnumerable<Account> GetAccountsByCustomer(int customerId);
        Task<IEnumerable<Account>> GetAccountsByCustomerAsync(int customerId, CancellationToken token);
        IEnumerable<Customer> GetAllCustomers();
        Task<IEnumerable<Customer>> GetAllCustomersAsync(CancellationToken token);
        void RegisterNewCustomer(Customer customer);
        Task RegisterNewCustomerAsync(Customer customer, CancellationToken token);
        void RemoveCustomer(int customerId);
        Task RemoveCustomerAsync(int customerId, CancellationToken token);
        void UpdateCustomer(Customer customer);
        Task UpdateCustomerAsync(Customer customer, CancellationToken token);
    }
}
