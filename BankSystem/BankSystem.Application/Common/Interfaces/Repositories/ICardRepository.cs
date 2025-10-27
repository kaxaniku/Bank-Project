using BankSystem.Domain.Entities;

namespace BankSystem.Application.Common.Interfaces.Repositories;

public interface ICardRepository : IRepositoryBase<Card>
{
    Task<Card?> GetByCardNumberAsync(string cardNumber, CancellationToken cancellationToken = default);
    Task<IEnumerable<Card>> GetAccountCardsAsync(int accountId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Card>> GetExpiredCardsAsync(CancellationToken cancellationToken = default);
}