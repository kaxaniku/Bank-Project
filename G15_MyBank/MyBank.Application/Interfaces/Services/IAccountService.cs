namespace MyBank.Application.Interfaces.Services;

public interface IAccountService
{
    decimal CheckBalance(int accountId);
    Task<decimal> CheckBalanceAsync(int accountId);
    void CloseAccount(int accountId);
    Task CloseAccountAsync(int accountId);
    void DepositMoney(int accountId, decimal amount);
    Task DepositMoneyAsync(int accountId, decimal amount);
    void FreezeAccount(int accountId);
    Task FreezeAccountAsync(int accountId);
    IEnumerable<int> ListAccountsByCustomer(int customerId);
    Task<IEnumerable<int>> ListAccountsByCustomerAsync(int customerId);
    void OpenNewAccount(int customerId);
    Task OpenNewAccountAsync(int customerId);
    void UnfreezeAccount(int accountId);
    Task UnfreezeAccountAsync(int accountId);
    void WithdrawMoney(int accountId, decimal amount);
    Task WithdrawMoneyAsync(int accountId, decimal amount);
}