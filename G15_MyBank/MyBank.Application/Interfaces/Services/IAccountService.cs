namespace MyBank.Application.Interfaces.Services;

public interface IAccountService
{
    decimal CheckBalance(int accountId);
    Task<decimal> CheckBalanceAsync(int accountId, CancellationToken cancellationToken);
    void CloseAccount(int accountId);
    Task CloseAccountAsync(int accountId, CancellationToken cancellationToken);
    void DepositMoney(int accountId, decimal amount);
    Task DepositMoneyAsync(int accountId, decimal amount, CancellationToken cancellationToken);
    void FreezeAccount(int accountId);
    Task FreezeAccountAsync(int accountId, CancellationToken cancellationToken);
    IEnumerable<int> ListAccountsByCustomer(int customerId);
    Task<IEnumerable<int>> ListAccountsByCustomerAsync(int customerId, CancellationToken cancellationToken);
    void OpenNewAccount(int customerId);
    Task OpenNewAccountAsync(int customerId, CancellationToken cancellationToken);
    void UnfreezeAccount(int accountId);
    Task UnfreezeAccountAsync(int accountId, CancellationToken cancellationToken);
    void WithdrawMoney(int accountId, decimal amount);
    Task WithdrawMoneyAsync(int accountId, decimal amount, CancellationToken cancellationToken);
}