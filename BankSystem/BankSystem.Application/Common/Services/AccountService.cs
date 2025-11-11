using BankSystem.Application.Common.DTOs.Account;
using BankSystem.Application.Common.Interfaces.Repositories;
using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Domain.Entities;
using BankSystem.Domain.Enums;
using BankSystem.Shared.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Security.Principal;

namespace BankSystem.Application.Common.Services;

public class AccountService(
    IUnitOfWork unitOfWork,
    ILogger<CustomerService> logger,
    IAccountNumberGenerator accountNumberGenerator) : IAccountService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<CustomerService> _logger = logger;
    private readonly IAccountNumberGenerator _accountNumberGenerator = accountNumberGenerator;

    public async Task<Result<Account>> CreateAccountAsync(Account account, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new account");

        var existingCustomer = await _unitOfWork.Customers
            .FirstOrDefaultAsync(c => c.Id == account.CustomerId && c.IsActive, cancellationToken);

        if (existingCustomer == null)
        {
            _logger.LogInformation("Customer with id {CustomerId} was not found", account.CustomerId);
            return BuildResult<Account>(404, false, messages: ["Customer not found."]);
        }

        var existingAccount = await _unitOfWork.Accounts.GetByUserCurrencyAndTypeAsync(
            account.CustomerId,
            account.Currency,
            account.Type,
            cancellationToken);

        if (existingAccount != null)
        {
            _logger.LogInformation(
                "User {UserId} already has a {AccountType} account in {Currency}",
                account.CustomerId, account.Type, account.Currency);
            return BuildResult<Account>(409, false,
                messages: [$"You already have a {account.Type} account in {account.Currency}."]);
        }

        account.AccountNumber = await _accountNumberGenerator.GenerateUniqueAccountNumberAsync(cancellationToken);
        account.Status = AccountStatus.Active;

        await _unitOfWork.Accounts.AddAsync(account, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created {AccountType} account in {Currency} with ID: {AccountId} and account number: {AccountNumber}",
            account.Type, account.Currency, account.Id, account.AccountNumber);
        return BuildResult(201, true, account, ["Account created successfully."]);
    }

    public async Task<Result<Account?>> GetAccountByIdAsync(int accountId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving account with ID: {AccountId} and it's cards", accountId);

        var account = await _unitOfWork.Accounts.GetByAccountIdAsync(accountId, cancellationToken);
        return account == null ? BuildResult<Account?>(404, false, messages: ["Account not found."])
            : BuildResult<Account?>(200, true, account, ["Account retrieved successfully."]);
    }

    public async Task<Result<IEnumerable<Account?>>> GetAccountByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving accounts with customer ID: {CustomerId} and it's cards", customerId);

        var accounts = await _unitOfWork.Accounts.GetCustomerAccountsAsync(customerId, cancellationToken);

        if (!accounts.Any())
        {
            _logger.LogInformation("No accounts found for customer ID: {CustomerId}", customerId);
            return BuildResult<IEnumerable<Account?>>(404, false, messages: ["No accounts found for the specified customer."]);
        }

        return BuildResult<IEnumerable<Account?>>(200, true, accounts, ["Accounts retrieved successfully."]);
    }

    public async Task<Result<Account?>> GetAccountByNumberAsync(string accountNumber, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving account with account number: {accountNumber} and it's cards", accountNumber);

        var account = await _unitOfWork.Accounts.GetByAccountNumberAsync(accountNumber, cancellationToken);
        return account == null ? BuildResult<Account?>(404, false, messages: ["Account not found."])
            : BuildResult<Account?>(200, true, account, ["Account retrieved successfully."]);
    }

    public async Task<Result<Account>> SuspendAccountAsync(int accountId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Suspending account with ID: {AccountId}", accountId);
        var account = await _unitOfWork.Accounts.GetByAccountIdAsync(accountId, cancellationToken);

        if (account == null)
        {
            return BuildResult<Account>(404, false, messages: ["Account not found."]);
        }

        if (account.Status != AccountStatus.Active)
        {
            _logger.LogWarning(
                "Cannot suspend account {AccountId} with status {Status}",
                accountId, account.Status);
            return BuildResult<Account>(400, false,
                messages: [$"Cannot suspend account with status {account.Status}. Only active accounts can be suspended."]);
        }

        account.Status = AccountStatus.Suspended;
        account.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Account {AccountId} suspended.",
            accountId);

        return BuildResult(200, true, account, ["Account suspended successfully."]);
    }

    public async Task<Result<Account>> ReactivateAccountAsync(int accountId, CancellationToken cancellationToken = default)
    {
        var account = await _unitOfWork.Accounts.GetByAccountIdAsync(accountId, cancellationToken);

        if (account == null)
        {
            return BuildResult<Account>(404, false, messages: ["Account not found."]);
        }

        if (account.Status != AccountStatus.Suspended)
        {
            _logger.LogWarning(
                "Cannot reactivate account {AccountId} with status {Status}",
                accountId, account.Status);
            return BuildResult<Account>(400, false,
                messages: [$"Cannot reactivate account with status {account.Status}. Only suspended accounts can be reactivated."]);
        }

        account.Status = AccountStatus.Active;
        account.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Account {AccountId} reactivated", accountId);

        return BuildResult(200, true, account, ["Account reactivated successfully."]);
    }

    public async Task<Result<Account>> CloseAccountAsync(int accountId, CancellationToken cancellationToken = default)
    {
        var account = await _unitOfWork.Accounts.GetByAccountIdAsync(accountId, cancellationToken);

        if (account == null)
        {
            return BuildResult<Account>(404, false, messages: ["Account not found."]);
        }

        if (account.Status == AccountStatus.Closed)
        {
            return BuildResult<Account>(400, false,
                messages: ["Account is already closed."]);
        }

        if (account.Balance != 0)
        {
            _logger.LogWarning(
                "Cannot close account {AccountId} with non-zero balance: {Balance}",
                accountId, account.Balance);

            return BuildResult<Account>(400, false,
                messages: [$"Cannot close account with balance {account.Balance:C}. Balance must be zero."]);
        }

        var activeCards = account.Cards?.Where(c => c.Status == CardStatus.Active).ToList();
        if (activeCards != null && activeCards?.Count != 0)
        {
            foreach (var card in activeCards!)
            {
                card.Status = CardStatus.Cancelled;
                card.UpdatedAt = DateTime.UtcNow;
            }

            _logger.LogInformation(
                "Cancelled {CardCount} active cards for account {AccountId}",
                activeCards.Count, accountId);
        }

        account.Status = AccountStatus.Closed;
        account.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Account {AccountId} closed.",
            accountId);

        return BuildResult(200, true, account, ["Account closed successfully."]);
    }

    public async Task<Result<Unit>> SoftDeleteAccountAsync(int accountId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Soft deleting account with ID: {AccountId}", accountId);

        var account = await _unitOfWork.Accounts.FirstOrDefaultAsync(c => c.Id == accountId, cancellationToken);
        if (account == null)
        {
            _logger.LogInformation("Attempt to soft delete non-existing account with ID: {AccountId}", accountId);
            return BuildResult<Unit>(404, false, messages: ["Account doesn't exist."]);
        }

        if (!account.IsActive)
        {
            return BuildResult<Unit>(409, false, messages: ["Account is already inactive."]);
        }

        var activeCards = await _unitOfWork.Cards.GetAllAsync(a => a.AccountId == accountId && a.IsActive, cancellationToken: cancellationToken);
        if (activeCards.Any())
        {
            _logger.LogInformation("Attempt to soft delete account with existing cards. Account ID: {AccountId}", accountId);
            return BuildResult<Unit>(409, false, messages: ["Can't delete account with active cards. Please deactivate all cards first."]);
        }

        _unitOfWork.Accounts.SoftDelete(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Soft deleted account with ID: {AccountId}", accountId);
        return BuildResult(200, true, new Unit(), ["Account has been successfully deactivated."]);
    }

    public async Task<Result<Unit>> RestoreAccountAsync(int accountId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Restoring account with ID: {AccountId}", accountId);
        var account = await _unitOfWork.Accounts.FirstOrDefaultAsync(c => c.Id == accountId, cancellationToken);

        if (account == null)
        {
            _logger.LogInformation("Attempt to restore non-existing account with ID: {AccountId}", accountId);
            return BuildResult<Unit>(404, false, messages: ["Account doesn't exist."]);
        }

        if (account.IsActive)
        {
            _logger.LogInformation("Attempt to restore already active account with ID: {AccountId}", accountId);
            return BuildResult<Unit>(409, false, messages: ["Account is already active."]);
        }

        _unitOfWork.Accounts.Restore(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Account restored. ID: {AccountId}", accountId);
        return BuildResult(200, true, new Unit(), ["Account restored successfully."]);
    }

    public async Task<Result<IEnumerable<BalancePerCurrency>>> GetAccountBalanceAsync(int customerId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving accounts with customer ID: {CustomerId} and it's cards", customerId);

        var accounts = await _unitOfWork.Accounts.GetCustomerAccountsAsync(customerId, cancellationToken);

        if (!accounts.Any())
        {
            _logger.LogInformation("No accounts found for customer ID: {CustomerId}", customerId);
            return BuildResult<IEnumerable<BalancePerCurrency>>(404, false, messages: ["No accounts found for the specified customer."]);
        }

        var balances = accounts
            .GroupBy(a => a.Currency)
            .Select(g => new BalancePerCurrency
            {
                Currency = g.Key,
                Balance = g.Sum(a => a.Balance)
            });

        return BuildResult(200, true, balances, ["Account balance retrieved successfully."]);
    }

    private static Result<T> BuildResult<T>(int code, bool succeeded, T? data = default, List<string>? messages = null)
    {
        return new Result<T>
        {
            Succeeded = succeeded,
            Code = code,
            Data = data ?? default!,
            Messages = messages ?? []
        };
    }
}
