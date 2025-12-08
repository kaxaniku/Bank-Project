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
        
        public void AddNewCard(string cardNumber, CardType cardType, DateTime exparationDate, string cvc, int accountId)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) || string.IsNullOrEmpty(cvc) || cvc.Length != 3)
                throw new ArgumentException("Please insert proper card number or cvc");

            if (_unitOfWork.CardRepository.Query(c => c.Account.AccountId == accountId && c.CardNumber == cardNumber).Any())
                throw new InvalidOperationException("Card with this number already exists for this account.");

            if (exparationDate < DateTime.UtcNow)
                throw new ArgumentException("Expiration date cannot be in the past.");

            Account account = _unitOfWork.AccountRepository.GetById(accountId)!;

            Card card = new()
            {
                CardNumber = cardNumber,
                CardType = (CardType)cardType,
                ExpirationDate = exparationDate,
                CVC = cvc,
                Account = account
            };

            _unitOfWork.CardRepository.Insert(card);
            _unitOfWork.SaveChanges();
            OnCardOpened(card);
        }

        public async Task AddNewCardAsync(string cardNumber, CardType cardType, DateTime exparationDate, string cvc, int accountId, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) || string.IsNullOrEmpty(cvc) || cvc.Length != 3)
                throw new ArgumentException("Please insert proper card number or cvc");

            if (exparationDate < DateTime.UtcNow)
                throw new ArgumentException("Expiration date cannot be in the past.");

            Account account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, token)!;

            Card card = new()
            {
                CardNumber = cardNumber,
                CardType = (CardType)cardType,
                ExpirationDate = exparationDate,
                CVC = cvc,
                Account = account
            };
            await _unitOfWork.CardRepository.InsertAsync(card, token);
            await _unitOfWork.SavechangesAsync(token);
            OnCardOpened(card);
        }

        public void ActivateCard(string cardNumber)
        {
            Card card = _unitOfWork.CardRepository.Query(c => c.CardNumber.Equals(cardNumber)).FirstOrDefault()!;

            ArgumentNullException.ThrowIfNullOrWhiteSpace(cardNumber, $"Card with number {cardNumber} not found.");

            if (card.Status == CardStatus.Active)
                throw new InvalidOperationException($"Card with card number {cardNumber} is already activated");

            card.Status = CardStatus.Active;

            _unitOfWork.CardRepository.Update(card);
            _unitOfWork.SaveChanges();
            OnCardUpdated(card);

        }

        public async Task ActivateCardAsync(string cardNumber, CancellationToken token)
        {
            Card card = (await _unitOfWork.CardRepository.QueryAsync(c => c.CardNumber.Equals(cardNumber), token)).FirstOrDefault()!;

            ArgumentNullException.ThrowIfNullOrWhiteSpace(cardNumber, $"Card with number {cardNumber} not found.");

            if (card.Status == CardStatus.Active)
                throw new InvalidOperationException($"Card with number {cardNumber} is already activated");

            card.Status = CardStatus.Active;

            await _unitOfWork.CardRepository.UpdateAsync(card, token);
            await _unitOfWork.SavechangesAsync(token);
            OnCardUpdated(card);
        }

        public void BlockCard(string cardNumber)
        {
            Card card = _unitOfWork.CardRepository.Query(c => c.CardNumber.Equals(cardNumber)).FirstOrDefault()!;

            ArgumentNullException.ThrowIfNullOrWhiteSpace(cardNumber, $"Card with number {cardNumber} not found.");

            if (card.Status == CardStatus.Suspended)
                throw new InvalidOperationException($"Card with number {cardNumber} is already suspended");

            card.Status = CardStatus.Suspended;

            _unitOfWork.CardRepository.Update(card);
            _unitOfWork.SaveChanges();
            OnCardUpdated(card);
        }

        public async Task BlockCardAsync(string cardNumber, CancellationToken token)
        {
            Card card = (await _unitOfWork.CardRepository.QueryAsync(c => c.CardNumber.Equals(cardNumber), token)).FirstOrDefault()!;

            ArgumentNullException.ThrowIfNullOrWhiteSpace(cardNumber, $"Card with number {cardNumber} not found.");

            if (card.Status == CardStatus.Suspended)
                throw new InvalidOperationException($"Card with number {cardNumber} is already suspended");

            card.Status = CardStatus.Suspended;

            await _unitOfWork.CardRepository.UpdateAsync(card, token);
            await _unitOfWork.SavechangesAsync(token);
            OnCardUpdated(card);
        }

        public IEnumerable<Card> GetCardsByAccount(string accountNumber)
        {
            return _unitOfWork.CardRepository.Query(a => a.Account.AccountNumber.Equals(accountNumber)).ToList();
        }

        public async Task<IEnumerable<Card>> GetCardsByAccountAsync(string accountNumber, CancellationToken token)
        {
            var cards = await _unitOfWork.CardRepository.QueryAsync(a => a.Account.AccountNumber.Equals(accountNumber), token);
           
            return cards.ToList();
        }

        public void UnblockCard(string cardNumber)
        {
            Card card = _unitOfWork.CardRepository.Query(c => c.CardNumber.Equals(cardNumber)).FirstOrDefault()!;

            ArgumentNullException.ThrowIfNullOrWhiteSpace(cardNumber, $"Card with number {cardNumber} not found.");

            if (card.Status == CardStatus.Active)
                throw new InvalidOperationException($"Card with Id {cardNumber} is already active");

            card.Status = CardStatus.Active;

            _unitOfWork.CardRepository.Update(card);
            _unitOfWork.SaveChanges();
            OnCardUpdated(card);
        }

        public async Task UnblockCardAsync(string cardNumber, CancellationToken token)
        {
            Card card = (await _unitOfWork.CardRepository.QueryAsync(c => c.CardNumber.Equals(cardNumber), token)).FirstOrDefault()!;

            ArgumentNullException.ThrowIfNullOrWhiteSpace(cardNumber, $"Card with number {cardNumber} not found.");

            if (card.Status == CardStatus.Active)
                throw new InvalidOperationException($"Card with number {cardNumber} is already active");

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
