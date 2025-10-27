using BankSystem.Domain.Entities;
using BankSystem.Shared.Models;

namespace BankSystem.Application.Common.Interfaces.Services;

public interface ICustomerService
{
    Task<Result<Customer>> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
    Task<bool> DeleteCustomerAsync(int customerId, CancellationToken cancellationToken = default);
    Task<(IEnumerable<Customer> Customers, int TotalCount)> GetCustomersPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<Customer?> GetCustomerWithAccountsAsync(int customerId, CancellationToken cancellationToken = default);
    Task UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
}