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
        void FreezeAccount(int accountId);
        Task FreezeAccountAsync(int accountId, CancellationToken token);
        void UnfreezeAccount(int accountId);
        Task UnfreezeAccountAsync(int accountId, CancellationToken token);
        IEnumerable<Account> GetAccountsByCostumer(int customerId);
        Task<IEnumerable<Account>> GetAccountsByCustomerAsync(int customerId, CancellationToken token);
    }
}
