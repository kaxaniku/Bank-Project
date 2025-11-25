namespace MyBank.Application.Interfaces.Services;

public interface ICardService
{
    void ActivateCard(int cardId);
    Task ActivateCardAsync(int cardId, CancellationToken cancellationToken);
    void BlockCard(int cardId);
    Task BlockCardAsync(int cardId, CancellationToken cancellationToken);
    void IssueNewCard(int accountId);
    Task IssueNewCardAsync(int accountId, CancellationToken cancellationToken);
    IEnumerable<int> ListCardsByAccount(int accountId);
    Task<IEnumerable<int>> ListCardsByAccountAsync(int accountId, CancellationToken cancellationToken);
    void UnblockCard(int cardId);
    Task UnblockCardAsync(int cardId, CancellationToken cancellationToken);
}