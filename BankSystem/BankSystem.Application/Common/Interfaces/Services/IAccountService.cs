using BankSystem.Application.Common.DTOs.Account;
using BankSystem.Domain.Entities;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Common.Interfaces.Services;

public interface IAccountService
{
    Task<Result<Account>> CreateAccountAsync(Account account, CancellationToken cancellationToken = default);
    Task<Result<Account?>> GetAccountByIdAsync(int accountId, CancellationToken cancellationToken = default);
    Task<Result<Account?>> GetAccountByNumberAsync(string accountNumber, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<Account?>>> GetAccountByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<BalancePerCurrency>>> GetAccountBalanceAsync(int customerId, CancellationToken cancellationToken = default);
    Task<Result<Account>> SuspendAccountAsync(int accountId, CancellationToken cancellationToken = default);
    Task<Result<Account>> ReactivateAccountAsync(int accountId, CancellationToken cancellationToken = default);
    Task<Result<Account>> CloseAccountAsync(int accountId, CancellationToken cancellationToken = default);
    Task<Result<Unit>> SoftDeleteAccountAsync(int accountId, CancellationToken cancellationToken = default);
    Task<Result<Unit>> RestoreAccountAsync(int accountId, CancellationToken cancellationToken = default);
}
