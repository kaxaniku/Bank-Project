using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services
{
    public interface ICardService 
    {
        void ActivateCard(string cardNumber);
        Task ActivateCardAsync(string cardNumber, CancellationToken token);
        void BlockCard(string cardNumber);
        Task BlockCardAsync(string cardNumber, CancellationToken token);
        void UnblockCard(string cardNumber);
        Task UnblockCardAsync(string cardNumber, CancellationToken token);
        void AddNewCard(string cardNumber, CardType cardType, DateTime exparationDate, string cvc, int accountId);
        Task AddNewCardAsync(string cardNumber, CardType cardType, DateTime exparationDate, string cvc, int accountId, CancellationToken token);
        IEnumerable<Card> GetCardsByAccount(string accountNumber);
        Task<IEnumerable<Card>> GetCardsByAccountAsync(string accountNumber, CancellationToken token);
    }
}
