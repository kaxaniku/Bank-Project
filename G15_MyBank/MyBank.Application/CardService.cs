using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.Application
{
    public class CardService : ICardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public event Action<Card>? CardOpened;
        public event Action<Card>? CardUpdated;
        public event Action<int>? CardClosed;

        public CardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public void AddNewCard(Card card)
        {
            if (card == null)
                throw new ArgumentNullException("Card cannot be null");

            _unitOfWork.CardRepository.Insert(card);
            _unitOfWork.SaveChanges();
            OnCardOpened(card);
        }

        public async Task AddNewCardAsync(Card card, CancellationToken token)
        {
            if (card == null)
                throw new ArgumentNullException("Card cannot be null");

            await _unitOfWork.CardRepository.InsertAsync(card, token);
            await _unitOfWork.SavechangesAsync(token);
            OnCardOpened(card);
        }

        public void ActivateCard(int cardId)
        {
            Card card = _unitOfWork.CardRepository.GetById(cardId)!;

            if (card == null)
                throw new KeyNotFoundException($"Card with Id {cardId} not found");

            if (card.Status == CardStatus.Active)
                throw new InvalidOperationException($"Card with Id {cardId} is already activated");

            card.Status = CardStatus.Active;

            _unitOfWork.CardRepository.Update(card);
            _unitOfWork.SaveChanges();
            OnCardUpdated(card);

        }

        public async Task ActivateCardAsync(int cardId, CancellationToken token)
        {
            Card card = await _unitOfWork.CardRepository.GetByIdAsync(cardId, token);

            if (card == null)
                throw new KeyNotFoundException($"Card with Id {cardId} not found");

            if (card.Status == CardStatus.Active)
                throw new InvalidOperationException($"Card with Id {cardId} is already activated");

            card.Status = CardStatus.Active;

            await _unitOfWork.CardRepository.UpdateAsync(card, token);
            await _unitOfWork.SavechangesAsync(token);
            OnCardUpdated(card);
        }

        public void BlockCard(int cardId)
        {
            Card card = _unitOfWork.CardRepository.GetById(cardId)!;

            if (card == null)
                throw new KeyNotFoundException($"Card with Id {cardId} not found");

            if (card.Status == CardStatus.Suspended)
                throw new InvalidOperationException($"Card with Id {cardId} is already suspended");

            card.Status = CardStatus.Suspended;

            _unitOfWork.CardRepository.Update(card);
            _unitOfWork.SaveChanges();
            OnCardUpdated(card);
        }

        public async Task BlockCardAsync(int cardId, CancellationToken token)
        {
            Card card = await _unitOfWork.CardRepository.GetByIdAsync(cardId, token);

            if (card == null)
                throw new KeyNotFoundException($"Card with Id {cardId} not found");

            if (card.Status == CardStatus.Suspended)
                throw new InvalidOperationException($"Card with Id {cardId} is already suspended");

            card.Status = CardStatus.Suspended;

            await _unitOfWork.CardRepository.UpdateAsync(card, token);
            await _unitOfWork.SavechangesAsync(token);
            OnCardUpdated(card);
        }

        public IEnumerable<Card> GetCardsByAccount(int accountId)
        {
            return _unitOfWork.CardRepository.Query(a => a.Account.AccountId == accountId).ToList();
        }

        public async Task<IEnumerable<Card>> GetCardsByAccountAsync(int accountId, CancellationToken token)
        {
            return await _unitOfWork.CardRepository.QueryAsync(a => a.Account.AccountId == accountId, token);
        }

        public void UnblockCard(int cardId)
        {
            Card card = _unitOfWork.CardRepository.GetById(cardId)!;

            if (card == null)
                throw new KeyNotFoundException($"Card with Id {cardId} not found");

            if (card.Status == CardStatus.Active)
                throw new InvalidOperationException($"Card with Id {cardId} is already active");

            card.Status = CardStatus.Active;

            _unitOfWork.CardRepository.Update(card);
            _unitOfWork.SaveChanges();
            OnCardUpdated(card);
        }

        public async Task UnblockCardAsync(int cardId, CancellationToken token)
        {
            Card card = await _unitOfWork.CardRepository.GetByIdAsync(cardId, token);

            if (card == null)
                throw new KeyNotFoundException($"Card with Id {cardId} not found");

            if (card.Status == CardStatus.Active)
                throw new InvalidOperationException($"Card with Id {cardId} is already active");

            card.Status = CardStatus.Active;

            await _unitOfWork.CardRepository.UpdateAsync(card, token);
            await _unitOfWork.SavechangesAsync(token);
            OnCardUpdated(card);
        }

        private void OnCardOpened(Card card)
        {
            CardOpened?.Invoke(card);
        }

        private void OnCardUpdated(Card card)
        {
            CardUpdated?.Invoke(card);
        }

        private void OnCardClosed(int cardId)
        {
            CardClosed?.Invoke(cardId);
        }
    }
}
