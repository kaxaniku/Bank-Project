using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services;

public interface IAccountService
{
    void ActivateAccount(string accountNum);
    Task ActivateAccountAsync(string accountNum, CancellationToken cancellationToken);
    void DeactivateAccount(string accountNum);
    Task DeactivateAccountAsync(string accountNum, CancellationToken cancellationToken);
    void BlockAccount(string accountNum);
    Task BlockAccountAsync(string accountNum, CancellationToken cancellationToken);
    decimal CheckBalance(string accountNum);
    Task<decimal> CheckBalanceAsync(string accountNum, CancellationToken cancellationToken);
    void CloseAccount(string accountNum);
    Task CloseAccountAsync(string accountNum, CancellationToken cancellationToken);
    void OpenNewAccount(int customerId, string accountNumber, decimal initialBalance);
    Task OpenNewAccountAsync(int customerId, string accountNumber, decimal initialBalance, CancellationToken cancellationToken);
    Account FindAccount(string accountNum);
    IAsyncEnumerable<Account> FindAccountAsync(string accountNum, CancellationToken cancellationToken);
}