using BankSystem.Domain.Entities;
using BankSystem.Domain.Enums;

namespace BankSystem.Application.Common.Interfaces.Repositories;

public interface IAccountRepository : IRepositoryBase<Account>
{
    Task<Account?> GetByAccountIdAsync(int accountId, CancellationToken cancellationToken = default);
    Task<Account?> GetByAccountNumberAsync(string accountNumber, CancellationToken cancellationToken = default);
    Task<IEnumerable<Account>> GetCustomerAccountsAsync(int customerId, CancellationToken cancellationToken = default);
    Task<decimal> GetTotalBalanceAsync(int customerId, CancellationToken cancellationToken = default);
    Task<Account?> GetByUserCurrencyAndTypeAsync(
        int customerId,
        string currency,
        AccountType accountType,
        CancellationToken cancellationToken = default);
}