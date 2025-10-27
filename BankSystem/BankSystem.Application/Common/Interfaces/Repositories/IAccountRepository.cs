using BankSystem.Domain.Entities;

namespace BankSystem.Application.Common.Interfaces.Repositories;

public interface IAccountRepository : IRepositoryBase<Account>
{
    Task<Account?> GetByAccountNumberAsync(string accountNumber, CancellationToken cancellationToken = default);
    Task<IEnumerable<Account>> GetCustomerAccountsAsync(int customerId, CancellationToken cancellationToken = default);
    Task<decimal> GetTotalBalanceAsync(int customerId, CancellationToken cancellationToken = default);
}