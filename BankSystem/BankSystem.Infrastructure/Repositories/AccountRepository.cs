using BankSystem.Application.Common.Interfaces.Repositories;
using BankSystem.Domain.Entities;
using BankSystem.Domain.Enums;
using BankSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Repositories;

internal class AccountRepository(BankSystemDbContext context) : RepositoryBase<Account>(context), IAccountRepository
{
    public async Task<Account?> GetByAccountIdAsync(int accountId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(a => a.Customer)
            .Include(a => a.Cards)
            .FirstOrDefaultAsync(a => a.Id == accountId && a.IsActive, cancellationToken);
    }

    public async Task<Account?> GetByAccountNumberAsync(string accountNumber, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(a => a.Customer)
            .Include(a => a.Cards)
            .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber && a.IsActive, cancellationToken);
    }

    public async Task<Account?> GetByUserCurrencyAndTypeAsync(int customerId, string currency, AccountType accountType, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(a =>
            a.CustomerId == customerId &&
            a.Currency == currency &&
            a.Type == accountType &&
            a.IsActive, cancellationToken);
    }

    public async Task<IEnumerable<Account>> GetCustomerAccountsAsync(int customerId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(a => a.Customer)
            .Include(a => a.Cards)
            .Where(a => a.CustomerId == customerId && a.IsActive && a.Customer.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<decimal> GetTotalBalanceAsync(int customerId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(a => a.CustomerId == customerId && a.IsActive)
            .SumAsync(a => a.Balance, cancellationToken);
    }
}