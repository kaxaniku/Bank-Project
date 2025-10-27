using BankSystem.Application.Common.Interfaces.Repositories;
using BankSystem.Domain.Entities;
using BankSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Repositories;

public class AccountRepository(BankSystemDbContext context) : RepositoryBase<Account>(context), IAccountRepository
{
    public async Task<Account?> GetByAccountNumberAsync(string accountNumber, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber, cancellationToken);
    }

    public async Task<IEnumerable<Account>> GetCustomerAccountsAsync(int customerId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(a => a.CustomerId == customerId)
            .ToListAsync(cancellationToken);
    }

    public async Task<decimal> GetTotalBalanceAsync(int customerId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(a => a.CustomerId == customerId && a.IsActive)
            .SumAsync(a => a.Balance, cancellationToken);
    }
}