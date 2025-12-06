using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services;

public interface ICardService
{
    void ActivateCard(int cardId);
    Task ActivateCardAsync(int cardId, CancellationToken cancellationToken);
    void DeactivateCard(int cardId);
    Task DeactivateCardAsync(int cardId, CancellationToken cancellationToken);
    void SuspendCard(int cardId);
    Task SuspendCardAsync(int cardId, CancellationToken cancellationToken);
    void IssueNewCard(string cardNumber, int cardType, string cvc, DateTime ExpirationDate, string accountNum);
    Task IssueNewCardAsync(string cardNumber, int cardType, string cvc, DateTime ExpirationDate, string accountNum, CancellationToken cancellationToken);
    IEnumerable<Card> ListCardsByAccount(string accountNum);
    Task<IEnumerable<Card>> ListCardsByAccountAsync(string accountNum, CancellationToken cancellationToken);
    Card FindCard(int cardId);
    Task<Card> FindCardAsync(int cardId, CancellationToken cancellationToken);
}