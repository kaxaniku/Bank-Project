using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;
using Microsoft.EntityFrameworkCore;

namespace MyBank.Application;

public sealed class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerService _customerService;

    public static event Action<Account>? AccountOpened;
    public static event Action<Account>? AccountUpdated;
    public static event Action<int>? AccountClosed;

    public AccountService(IUnitOfWork unitOfWork, ICustomerService customerService)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
    }

    public void OpenNewAccount(int customerId, string accountNumber, decimal initialBalance)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(accountNumber);
        ArgumentOutOfRangeException.ThrowIfNegative(initialBalance, nameof(initialBalance));

        var customer = _customerService.FindCustomer(customerId);
        var account = new Account
        {
            Customer = customer,
            AccountNumber = accountNumber,
            Balance = initialBalance,
            Status = AccountStatus.Active
        };

        _unitOfWork.AccountRepository.Insert(account);
        _unitOfWork.SaveChanges();
        OnAccountOpened(account);
    }

    public void CloseAccount(string accountNum)
    {
        Account account = FindAccount(accountNum);

        _unitOfWork.AccountRepository.Delete(account);
        _unitOfWork.SaveChanges();
        OnAccountClosed(account.AccountId);
    }

    public void ActivateAccount(string accountNum)
    {
        Account account = FindAccount(accountNum);
        if (account.Status == AccountStatus.Active)
            throw new InvalidOperationException($"Account with number {accountNum} is already active.");
        account.Status = AccountStatus.Active;

        _unitOfWork.AccountRepository.Update(account);
        _unitOfWork.SaveChanges();
        OnAccountUpdated(account);
    }

    public void DeactivateAccount(string accountNum)
    {
        Account account = FindAccount(accountNum);
        if (account.Status == AccountStatus.Inactive)
            throw new InvalidOperationException($"Account with number {accountNum} is already inactive.");
        account.Status = AccountStatus.Inactive;

        _unitOfWork.AccountRepository.Update(account);
        _unitOfWork.SaveChanges();
        OnAccountUpdated(account);
    }

    public void BlockAccount(string accountNum)
    {
        Account account = FindAccount(accountNum);
        if (account.Status == AccountStatus.Blocked)
            throw new InvalidOperationException($"Account with number {accountNum} is already blocked.");
        account.Status = AccountStatus.Blocked;

        _unitOfWork.AccountRepository.Update(account);
        _unitOfWork.SaveChanges();
        OnAccountUpdated(account);
    }

    public decimal CheckBalance(string accountNum)
    {
        Account account = FindAccount(accountNum);

        return account.Balance;
    }

    public Account FindAccount(string accountNum)
    {
        Account account = _unitOfWork.AccountRepository.Query(x => x.AccountNumber == accountNum).FirstOrDefault()
            ?? throw new InvalidOperationException($"Account with number {accountNum} does not exist.");
        if (!account.Activity.IsActive)
            throw new InvalidOperationException($"Account with number {accountNum} no longer exists.");
        return account;
    }

    public async Task OpenNewAccountAsync(int customerId, string accountNumber, decimal initialBalance, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentNullException(nameof(accountNumber));
        if (initialBalance < 0)
            throw new ArgumentOutOfRangeException(nameof(initialBalance), "Initial balance must be non-negative.");

        var customer = await _customerService.FindCustomerAsync(customerId, cancellationToken);

        var account = new Account
        {
            Customer = customer!,
            AccountNumber = accountNumber,
            Balance = initialBalance,
            Status = AccountStatus.Active
        };

        await _unitOfWork.AccountRepository.InsertAsync(account, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnAccountOpened(account);
    }

    public async Task CloseAccountAsync(string accountNum, CancellationToken cancellationToken)
    {
        var account = await FindAccountAsync(accountNum, cancellationToken);
        await _unitOfWork.AccountRepository.DeleteAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnAccountClosed(account.AccountId);
    }

    public async Task ActivateAccountAsync(string accountNum, CancellationToken cancellationToken)
    {
        var account = await FindAccountAsync(accountNum, cancellationToken);
        if (account.Status == AccountStatus.Active)
            throw new InvalidOperationException($"Account with number {accountNum} is already active.");
        account.Status = AccountStatus.Active;

        await _unitOfWork.AccountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnAccountUpdated(account);
    }

    public async Task DeactivateAccountAsync(string accountNum, CancellationToken cancellationToken)
    {
        var account = await FindAccountAsync(accountNum, cancellationToken);
        if (account.Status == AccountStatus.Inactive)
            throw new InvalidOperationException($"Account with number {accountNum} is already inactive.");
        account.Status = AccountStatus.Inactive;

        await _unitOfWork.AccountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnAccountUpdated(account);
    }

    public async Task BlockAccountAsync(string accountNum, CancellationToken cancellationToken)
    {
        var account = await FindAccountAsync(accountNum, cancellationToken);
        if (account.Status == AccountStatus.Blocked)
            throw new InvalidOperationException($"Account with number {accountNum} is already blocked.");
        account.Status = AccountStatus.Blocked;

        await _unitOfWork.AccountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnAccountUpdated(account);
    }

    public async Task<decimal> CheckBalanceAsync(string accountNum, CancellationToken cancellationToken)
    {
        var account = await FindAccountAsync(accountNum, cancellationToken);
        return account.Balance;
    }


    public async Task<Account> FindAccountAsync(string accountNum, CancellationToken cancellationToken)
    {
        var accounts = await _unitOfWork.AccountRepository.QueryAsync(x => x.AccountNumber == accountNum, cancellationToken);
        Account account = accounts.FirstOrDefault()
            ?? throw new InvalidOperationException($"Account with number {accountNum} does not exist.");
        if (!account.Activity.IsActive)
            throw new InvalidOperationException($"Account with number {accountNum} no longer exists.");
        return account;
    }

    private static void OnAccountOpened(Account account)
    {
        AccountOpened?.Invoke(account);
    }

    private static void OnAccountUpdated(Account account)
    {
        AccountUpdated?.Invoke(account);
    }

    private static void OnAccountClosed(int accountNum)
    {
        AccountClosed?.Invoke(accountNum);
    }
}
