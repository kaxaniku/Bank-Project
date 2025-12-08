using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services
{
    internal interface ICustomerService 
    {
        Customer? FindCustomer(string personalNumber);
        Task<Customer?> FindCustomerAsync(string personalNumber, CancellationToken token);
        IEnumerable<Account> GetAccountsByCustomer(string personalNumber);
        Task<IEnumerable<Account>> GetAccountsByCustomerAsync(string personalNumber, CancellationToken token);
        IEnumerable<Customer> GetAllCustomers();
        Task<IEnumerable<Customer>> GetAllCustomersAsync(CancellationToken token);
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
        CancellationToken token);
        void RemoveCustomer(string personalNumber);
        Task RemoveCustomerAsync(string personalNumber, CancellationToken token);
        void UpdateCustomer(string personalNumber);
        Task UpdateCustomerAsync(string personalNumber, CancellationToken token);
    }
}
