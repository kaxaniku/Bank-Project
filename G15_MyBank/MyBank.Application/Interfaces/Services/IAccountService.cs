using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services;

public interface IAccountService
{
    void ActivateAccount(int accountId);
    Task ActivateAccountAsync(int accountId, CancellationToken cancellationToken);
    void DeactivateAccount(int accountId);
    Task DeactivateAccountAsync(int accountId, CancellationToken cancellationToken);
    void BlockAccount(int accountId);
    Task BlockAccountAsync(int accountId, CancellationToken cancellationToken);
    decimal CheckBalance(int accountId);
    Task<decimal> CheckBalanceAsync(int accountId, CancellationToken cancellationToken);
    void CloseAccount(int accountId);
    Task CloseAccountAsync(int accountId, CancellationToken cancellationToken);
    void DepositMoney(int accountId, decimal amount);
    Task DepositMoneyAsync(int accountId, decimal amount, CancellationToken cancellationToken);
    void OpenNewAccount(int customerId, string accountNumber, decimal initialBalance);
    Task OpenNewAccountAsync(Account account, CancellationToken cancellationToken);
    void WithdrawMoney(int accountId, decimal amount);
    Task WithdrawMoneyAsync(int accountId, decimal amount, CancellationToken cancellationToken);
}