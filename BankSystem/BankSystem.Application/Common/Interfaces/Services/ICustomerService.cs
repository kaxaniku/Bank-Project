using BankSystem.Domain.Entities;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Common.Interfaces.Services;

public interface ICustomerService
{
    Task<Result<Customer>> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
    Task<Result<Customer?>> GetCustomerWithAccountsAsync(int customerId, CancellationToken cancellationToken = default);
    Task<Result<Customer?>> GetCustomerWithAccountsAsync(string nationalId, CancellationToken cancellationToken = default);
    Task<(IEnumerable<Customer> Customers, int TotalCount)> GetCustomersPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<Result<Customer>> UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
    Task<Result<Unit>> HardDeleteCustomerAsync(int customerId, CancellationToken cancellationToken = default);
    Task<Result<Unit>> SoftDeleteCustomerAsync(int customerId, CancellationToken cancellationToken = default);
    Task<Result<Unit>> RestoreCustomerAsync(int customerId, CancellationToken cancellationToken = default);
}