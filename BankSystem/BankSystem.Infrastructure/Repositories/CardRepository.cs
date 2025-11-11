using BankSystem.Application.Common.Interfaces.Repositories;
using BankSystem.Domain.Entities;
using BankSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Repositories;

internal class CardRepository(BankSystemDbContext context) : RepositoryBase<Card>(context), ICardRepository
{
    public async Task<Card?> GetByCardNumberAsync(string cardNumber, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(c => c.CardNumber == cardNumber, cancellationToken);
    }

    public async Task<IEnumerable<Card>> GetAccountCardsAsync(int accountId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(c => c.AccountId == accountId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Card>> GetExpiredCardsAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        return await DbSet
            .Where(c => c.ExpirationDate < now)
            .ToListAsync(cancellationToken);
    }
}
