namespace MyBank.Application.Interfaces.Services;

public interface ICardService
{
    void ActivateCard(int cardId);
    Task ActivateCardAsync(int cardId);
    void BlockCard(int cardId);
    Task BlockCardAsync(int cardId);
    void IssueNewCard(int accountId);
    Task IssueNewCardAsync(int accountId);
    IEnumerable<int> ListCardsByAccount(int accountId);
    Task<IEnumerable<int>> ListCardsByAccountAsync(int accountId);
    void UnblockCard(int cardId);
    Task UnblockCardAsync(int cardId);
}