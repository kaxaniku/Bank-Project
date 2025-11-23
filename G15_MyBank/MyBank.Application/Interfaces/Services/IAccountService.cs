using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services
{
    internal interface IAccountService : IBaseService<Account>
    {
        void CheckBalance(Account account);
        Task DepositAsync(int id, decimal balance);
        Task WithdrawAsync(int id, decimal balance);
        Task FreezeAcoount(int id);
        Task UnfreezeAccount(int id);
        Task<List<Account>> GetAccountsByCustomerAsync(int id);
    }
}
