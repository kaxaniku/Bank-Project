using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services
{
    internal interface IAccountService 
    {
        void OpenNewAccount(Account account);
        Task OpenNewAccountAsync(Account account, CancellationToken token);
        decimal CheckBalance(int accountId);
        Task<decimal> CheckBalanceAsync(int accountId, CancellationToken token);
        void CloseAccount(int accountId);
        Task CloseAccountAsync(int accountId, CancellationToken token);
        void Deposit(int accountId, decimal amount);
        Task DepositAsync(int accountId, decimal amount, CancellationToken token);
        void FreezeAccount(int accountId);
        Task FreezeAccountAsync(int accountId, CancellationToken token);
        void UnfreezeAccount(int accountId);
        Task UnfreezeAccountAsync(int accountId, CancellationToken token);
        void Withdraw(int accountId, decimal amount);
        Task WithdrawAsync(int accountId, decimal amount, CancellationToken token);
        IEnumerable<Account> GetAccountsByCostumer(int customerId);
        Task<IEnumerable<Account>> GetAccountsByCustomerAsync(int customerId, CancellationToken token);
    }
}
