using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services;

public interface ICardService
{
    void ActivateCard(string cardNum);
    Task ActivateCardAsync(string cardNum, CancellationToken cancellationToken);
    void DeactivateCard(string cardNum);
    Task DeactivateCardAsync(string cardNum, CancellationToken cancellationToken);
    void SuspendCard(string cardNum);
    Task SuspendCardAsync(string cardNum, CancellationToken cancellationToken);
    void IssueNewCard(string cardNumber, byte cardType, string cvc, DateTime ExpirationDate, string accountNum);
    Task IssueNewCardAsync(string cardNumber, byte cardType, string cvc, DateTime ExpirationDate, string accountNum, CancellationToken cancellationToken);
    IEnumerable<Card> ListCardsByAccount(string accountNum);
    Task<IEnumerable<Card>> ListCardsByAccountAsync(string accountNum, CancellationToken cancellationToken);
    Card FindCard(string cardNum);
    Task<Card> FindCardAsync(string cardNum, CancellationToken cancellationToken);
    void CloseCard(string cardNum);
    Task CloseCardAsync(string cardNum, CancellationToken cancellationToken);
}