using BankSystem.Application.Common.Interfaces.Repositories;
using BankSystem.Domain.Entities;
using BankSystem.Domain.Enums;
using BankSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Repositories;

public class TransactionRepository(BankSystemDbContext context) : RepositoryBase<Transaction>(context), ITransactionRepository
{
    public async Task<IEnumerable<Transaction>> GetAccountTransactionsAsync(
        int accountId,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        CancellationToken cancellationToken = default)
    {
        var query = DbSet.Where(t =>
            t.SourceAccountId == accountId ||
            t.DestinationAccountId == accountId);

        if (fromDate.HasValue)
        {
            query = query.Where(t => t.CreatedAt >= fromDate.Value);
        }

        if (toDate.HasValue)
        {
            query = query.Where(t => t.CreatedAt <= toDate.Value);
        }

        return await query
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Transaction?> GetByReferenceAsync(string reference, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(t => t.SourceAccount)
            .Include(t => t.DestinationAccount)
            .FirstOrDefaultAsync(t => t.Reference == reference, cancellationToken);
    }

    public async Task<decimal> GetTotalIncomingAmountAsync(
        int accountId,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        CancellationToken cancellationToken = default)
    {
        var query = DbSet.Where(t =>
            t.DestinationAccountId == accountId &&
            t.Status == TransactionStatus.Completed);

        if (fromDate.HasValue)
        {
            query = query.Where(t => t.CreatedAt >= fromDate.Value);
        }

        if (toDate.HasValue)
        {
            query = query.Where(t => t.CreatedAt <= toDate.Value);
        }

        return await query.SumAsync(t => t.Amount, cancellationToken);
    }

    public async Task<decimal> GetTotalOutgoingAmountAsync(
        int accountId,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        CancellationToken cancellationToken = default)
    {
        var query = DbSet.Where(t =>
            t.SourceAccountId == accountId &&
            t.Status == TransactionStatus.Completed);

        if (fromDate.HasValue)
        {
            query = query.Where(t => t.CreatedAt >= fromDate.Value);
        }

        if (toDate.HasValue)
        {
            query = query.Where(t => t.CreatedAt <= toDate.Value);
        }

        return await query.SumAsync(t => t.Amount, cancellationToken);
    }
}