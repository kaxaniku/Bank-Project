using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;

namespace MyBank.Application;

public sealed class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public void OpenNewAccount(int customerId)
    {
        throw new NotImplementedException();
    }

    public void CloseAccount(int accountId)
    {
        throw new NotImplementedException();
    }

    public decimal CheckBalance(int accountId)
    {
        throw new NotImplementedException();
    }

    public void DepositMoney(int accountId, decimal amount)
    {
        throw new NotImplementedException();
    }

    public void WithdrawMoney(int accountId, decimal amount)
    {
        throw new NotImplementedException();
    }

    public void FreezeAccount(int accountId)
    {
        throw new NotImplementedException();
    }

    public void UnfreezeAccount(int accountId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<int> ListAccountsByCustomer(int customerId)
    {
        throw new NotImplementedException();
    }

    public Task OpenNewAccountAsync(int customerId)
    {
        throw new NotImplementedException();
    }

    public Task CloseAccountAsync(int accountId)
    {
        throw new NotImplementedException();
    }

    public Task<decimal> CheckBalanceAsync(int accountId)
    {
        throw new NotImplementedException();
    }

    public Task DepositMoneyAsync(int accountId, decimal amount)
    {
        throw new NotImplementedException();
    }

    public Task WithdrawMoneyAsync(int accountId, decimal amount)
    {
        throw new NotImplementedException();
    }

    public Task FreezeAccountAsync(int accountId)
    {
        throw new NotImplementedException();
    }

    public Task UnfreezeAccountAsync(int accountId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<int>> ListAccountsByCustomerAsync(int customerId)
    {
        throw new NotImplementedException();
    }
}
