using BankSystem.Domain.Entities;

namespace BankSystem.Application.Common.Interfaces.Repositories;

public interface ICustomerRepository : IRepositoryBase<Customer>
{
    Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Customer?> GetByNationalIdAsync(string nationalId, CancellationToken cancellationToken = default);
    Task<Customer?> GetWithAccountsAsync(int customerId, CancellationToken cancellationToken = default);
    Task<Customer?> GetByEmailOrNationalIdAsync(string email, string nationalId, CancellationToken cancellationToken);
}
