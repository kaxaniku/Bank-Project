using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services;

public interface ICardService
{
    void ActivateCard(int cardId);
    Task ActivateCardAsync(int cardId, CancellationToken cancellationToken);
    void BlockCard(int cardId);
    Task BlockCardAsync(int cardId, CancellationToken cancellationToken);
    void SuspendCard(int cardId);
    Task SuspendCardAsync(int cardId, CancellationToken cancellationToken);
    void IssueNewCard(Card card);
    Task IssueNewCardAsync(Card card, CancellationToken cancellationToken);
    IEnumerable<Card> ListCardsByAccount(int accountId);
    Task<IEnumerable<Card>> ListCardsByAccountAsync(int accountId, CancellationToken cancellationToken);
    Card FindCardById(int cardId);
    Task<Card> FindCardByIdAsync(int cardId, CancellationToken cancellationToken);
}