using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.Application;

public sealed class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;

    public static event Action<Account>? AccountOpened;
    public static event Action<Account>? AccountUpdated;
    public static event Action<int>? AccountClosed;

    public AccountService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public void OpenNewAccount(Account account)
    {
        if (account == null)
            throw new ArgumentNullException(nameof(account));
        _unitOfWork.AccountRepository.Insert(account);
        _unitOfWork.SaveChanges();
        OnAccountOpened(account);
    }

    public void CloseAccount(int accountId)
    {
        Account account = _unitOfWork.AccountRepository.GetById(accountId)
            ?? throw new InvalidOperationException($"Account with ID {accountId} does not exist.");
        _unitOfWork.AccountRepository.Delete(account);
        _unitOfWork.SaveChanges();
        OnAccountClosed(accountId);
    }

    public void ActivateAccount(int accountId)
    {
        Account account = _unitOfWork.AccountRepository.GetById(accountId)
            ?? throw new InvalidOperationException($"Account with ID {accountId} does not exist.");
        account.Status = AccountStatus.Active;
        _unitOfWork.AccountRepository.Update(account);
        _unitOfWork.SaveChanges();
        OnAccountUpdated(account);
    }

    public void DeactivateAccount(int accountId)
    {
        Account account = _unitOfWork.AccountRepository.GetById(accountId)
            ?? throw new InvalidOperationException($"Account with ID {accountId} does not exist.");
        account.Status = AccountStatus.Inactive;
        _unitOfWork.AccountRepository.Update(account);
        _unitOfWork.SaveChanges();
        OnAccountUpdated(account);
    }

    public void BlockAccount(int accountId)
    {
        Account account = _unitOfWork.AccountRepository.GetById(accountId)
            ?? throw new InvalidOperationException($"Account with ID {accountId} does not exist.");
        account.Status = AccountStatus.Blocked;
        _unitOfWork.AccountRepository.Update(account);
        _unitOfWork.SaveChanges();
        OnAccountUpdated(account);
    }

    public decimal CheckBalance(int accountId)
    {
        Account account = _unitOfWork.AccountRepository.GetById(accountId)
            ?? throw new InvalidOperationException($"Account with ID {accountId} does not exist.");
        return account.Balance;
    }

    public void DepositMoney(int accountId, decimal amount)
    {
        Account account = _unitOfWork.AccountRepository.GetById(accountId)
            ?? throw new InvalidOperationException($"Account with ID {accountId} does not exist.");
        account.Balance += amount;
        _unitOfWork.AccountRepository.Update(account);
        _unitOfWork.SaveChanges();
        OnAccountUpdated(account);
    }

    public void WithdrawMoney(int accountId, decimal amount)
    {
        Account account = _unitOfWork.AccountRepository.GetById(accountId)
            ?? throw new InvalidOperationException($"Account with ID {accountId} does not exist.");
        account.Balance -= amount;
        _unitOfWork.AccountRepository.Update(account);
        _unitOfWork.SaveChanges();
        OnAccountUpdated(account);
    }

    public async Task OpenNewAccountAsync(Account account, CancellationToken cancellationToken)
    {
        if (account == null)
            throw new ArgumentNullException(nameof(account));
        await _unitOfWork.AccountRepository.InsertAsync(account, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnAccountOpened(account);
    }

    public async Task CloseAccountAsync(int accountId, CancellationToken cancellationToken)
    {
        Account account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, cancellationToken)
            ?? throw new InvalidOperationException($"Account with ID {accountId} does not exist.");
        await _unitOfWork.AccountRepository.DeleteAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnAccountClosed(accountId);
    }

    public async Task ActivateAccountAsync(int accountId, CancellationToken cancellationToken)
    {
        Account account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, cancellationToken)
            ?? throw new InvalidOperationException($"Account with ID {accountId} does not exist.");
        account.Status = AccountStatus.Active;
        await _unitOfWork.AccountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnAccountUpdated(account);
    }

    public async Task DeactivateAccountAsync(int accountId, CancellationToken cancellationToken)
    {
        Account account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, cancellationToken)
            ?? throw new InvalidOperationException($"Account with ID {accountId} does not exist.");
        account.Status = AccountStatus.Inactive;
        await _unitOfWork.AccountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnAccountUpdated(account);
    }

    public async Task BlockAccountAsync(int accountId, CancellationToken cancellationToken)
    {
        Account account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, cancellationToken)
            ?? throw new InvalidOperationException($"Account with ID {accountId} does not exist.");
        account.Status = AccountStatus.Blocked;
        await _unitOfWork.AccountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnAccountUpdated(account);
    }

    public async Task<decimal> CheckBalanceAsync(int accountId, CancellationToken cancellationToken)
    {
        Account account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, cancellationToken)
            ?? throw new InvalidOperationException($"Account with ID {accountId} does not exist.");
        return account.Balance;
    }

    public async Task DepositMoneyAsync(int accountId, decimal amount, CancellationToken cancellationToken)
    {
        Account account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, cancellationToken)
            ?? throw new InvalidOperationException($"Account with ID {accountId} does not exist.");
        account.Balance += amount;
        await _unitOfWork.AccountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnAccountUpdated(account);
    }

    public async Task WithdrawMoneyAsync(int accountId, decimal amount, CancellationToken cancellationToken)
    {
        Account account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, cancellationToken)
                    ?? throw new InvalidOperationException($"Account with ID {accountId} does not exist.");
        account.Balance -= amount;
        await _unitOfWork.AccountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnAccountUpdated(account);
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
