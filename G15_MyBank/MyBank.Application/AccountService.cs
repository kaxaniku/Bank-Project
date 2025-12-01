using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

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

        var customer = _customerService.FindCustomerById(customerId);
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

    public void CloseAccount(int accountId)
    {
        Account account = FindAccount(accountId);

        _unitOfWork.AccountRepository.Delete(account);
        _unitOfWork.SaveChanges();
        OnAccountClosed(accountId);
    }

    public void ActivateAccount(int accountId)
    {
        Account account = FindAccount(accountId);
        if (account.Status == AccountStatus.Active)
            throw new InvalidOperationException($"Account with ID {accountId} is already active.");
        account.Status = AccountStatus.Active;

        _unitOfWork.AccountRepository.Update(account);
        _unitOfWork.SaveChanges();
        OnAccountUpdated(account);
    }

    public void DeactivateAccount(int accountId)
    {
        Account account = FindAccount(accountId);
        if (account.Status == AccountStatus.Inactive)
            throw new InvalidOperationException($"Account with ID {accountId} is already inactive.");
        account.Status = AccountStatus.Inactive;

        _unitOfWork.AccountRepository.Update(account);
        _unitOfWork.SaveChanges();
        OnAccountUpdated(account);
    }

    public void BlockAccount(int accountId)
    {
        Account account = FindAccount(accountId);
        if (account.Status == AccountStatus.Blocked)
            throw new InvalidOperationException($"Account with ID {accountId} is already blocked.");
        account.Status = AccountStatus.Blocked;

        _unitOfWork.AccountRepository.Update(account);
        _unitOfWork.SaveChanges();
        OnAccountUpdated(account);
    }

    public decimal CheckBalance(int accountId)
    {
        Account account = FindAccount(accountId);

        return account.Balance;
    }

    public Account FindAccount(int accountId)
    {
        Account account = _unitOfWork.AccountRepository.GetById(accountId)
            ?? throw new InvalidOperationException($"Account with ID {accountId} does not exist.");
        if (!account.Activity.IsActive)
            throw new InvalidOperationException($"Account with ID {accountId} no longer exists.");
        return account;
    }

    public async Task OpenNewAccountAsync(int customerId, string accountNumber, decimal initialBalance, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentNullException(nameof(accountNumber));
        if (initialBalance < 0)
            throw new ArgumentOutOfRangeException(nameof(initialBalance), "Initial balance must be non-negative.");

        var customer = await _customerService.FindCustomerByIdAsync(customerId, cancellationToken);

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

    public async Task CloseAccountAsync(int accountId, CancellationToken cancellationToken)
    {
        var account = await FindAccountByIdAsync(accountId, cancellationToken);
        await _unitOfWork.AccountRepository.DeleteAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnAccountClosed(accountId);
    }

    public async Task ActivateAccountAsync(int accountId, CancellationToken cancellationToken)
    {
        var account = await FindAccountByIdAsync(accountId, cancellationToken);
        account.Status = AccountStatus.Active;

        await _unitOfWork.AccountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnAccountUpdated(account);
    }

    public async Task DeactivateAccountAsync(int accountId, CancellationToken cancellationToken)
    {
        var account = await FindAccountByIdAsync(accountId, cancellationToken);
        account.Status = AccountStatus.Inactive;

        await _unitOfWork.AccountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnAccountUpdated(account);
    }

    public async Task BlockAccountAsync(int accountId, CancellationToken cancellationToken)
    {
        var account = await FindAccountByIdAsync(accountId, cancellationToken);
        account.Status = AccountStatus.Blocked;

        await _unitOfWork.AccountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnAccountUpdated(account);
    }

    public async Task<decimal> CheckBalanceAsync(int accountId, CancellationToken cancellationToken)
    {
        var account = await FindAccountByIdAsync(accountId, cancellationToken);
        return account.Balance;
    }

    public async Task<Account> FindAccountByIdAsync(int accountId, CancellationToken cancellationToken)
    {
        Account account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, cancellationToken)
            ?? throw new InvalidOperationException($"Account with ID {accountId} does not exist.");
        if (!account.Activity.IsActive)
            throw new InvalidOperationException($"Account with ID {accountId} no longer exists.");
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

    private static void OnAccountClosed(int accountId)
    {
        AccountClosed?.Invoke(accountId);
    }
}
