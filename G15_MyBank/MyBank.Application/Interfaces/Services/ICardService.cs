using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services
{
    internal interface ICardService 
    {
        void ActivateCard(int cardId);
        Task ActivateCardAsync(int cardId, CancellationToken token);
        void BlockCard(int cardId);
        Task BlockCardAsync(int cardId, CancellationToken token);
        void UnblockCard(int cardId);
        Task UnblockCardAsync(int cardId, CancellationToken token);
        void AddNewCard(Card card);
        Task AddNewCardAsync(Card card, CancellationToken token);
        IEnumerable<Card> GetCardsByAccount(int accountId);
        Task<IEnumerable<Card>> GetCardsByAccountAsync(int accountId, CancellationToken token);
    }
}
