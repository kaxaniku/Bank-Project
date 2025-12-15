using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services
{
    public interface IAccountService 
    {
        void OpenNewAccount(string personalNumber, string accounntNumber, decimal balance);
        Task OpenNewAccountAsync(string personalNumber, string accounntNumber, decimal balance, CancellationToken token);
        decimal CheckBalance(string accountNumber);
        Task<decimal> CheckBalanceAsync(string accountNumber, CancellationToken token);
        void CloseAccount(string accountNumber);
        Task CloseAccountAsync(string accountNumber, CancellationToken token);
        void FreezeAccount(string accountNumber);
        Task FreezeAccountAsync(string accountNumber, CancellationToken token);
        void UnfreezeAccount(string accountNumber);
        Task UnfreezeAccountAsync(string accountNumber, CancellationToken token);
        IEnumerable<Account> GetAccountsByCostumer(string personaNumber);
        Task<IEnumerable<Account>> GetAccountsByCustomerAsync(string personalNumber, CancellationToken token);
    }
}
