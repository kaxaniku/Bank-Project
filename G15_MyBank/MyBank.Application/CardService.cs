using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.Application;

public sealed class CardService : ICardService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccountService _accountService;

    public CardService(IUnitOfWork unitOfWork, IAccountService accountService)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
    }

    public static event Action<Card>? CardIssued;
    public static event Action<Card>? CardUpdated;
    public static event Action<int>? CardClosed;

    public void IssueNewCard(string cardNumber, int cardType, string cvc, DateTime ExpirationDate, string accountNum)
    {
        if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(cvc))
            throw new ArgumentException("Card number and CVC cannot be null or empty.");
        if (!Enum.IsDefined(typeof(CardType), cardType))
        {
            throw new ArgumentOutOfRangeException("Corresponding value of card type doesn't exist.");
        }
        if(ExpirationDate <= DateTime.UtcNow)
        {
            throw new ArgumentOutOfRangeException("Expiration date must be in the future.");
        }

        Account account = _accountService.FindAccount(accountNum);

        Card card = new Card
        {
            CardNumber = cardNumber,
            CardType = (CardType)cardType,
            CVC = cvc,
            ExpirationDate = ExpirationDate,
            Status = CardStatus.Inactive,
            Account = account,
            Activity = new ActivityInfo()
        };

        _unitOfWork.CardRepository.Insert(card);
        _unitOfWork.SaveChanges();
        OnCardIssued(card);
    }

    public void ActivateCard(int cardId)
    {
        Card card = FindCard(cardId);
        if (card.Status == CardStatus.Active)
            throw new InvalidOperationException($"Card with ID {cardId} is already active.");
        card.Status = CardStatus.Active;
        _unitOfWork.CardRepository.Update(card);
        _unitOfWork.SaveChanges();
        OnCardUpdated(card);
    }

    public void DeactivateCard(int cardId)
    {
        Card card = FindCard(cardId);
        if (card.Status == CardStatus.Inactive)
            throw new InvalidOperationException($"Card with ID {cardId} is already inactive.");
        card.Status = CardStatus.Inactive;
        _unitOfWork.CardRepository.Update(card);
        _unitOfWork.SaveChanges();
        OnCardUpdated(card);
    }

    public void SuspendCard(int cardId)
    {
        Card card = FindCard(cardId);
        if (card.Status == CardStatus.Suspended)
            throw new InvalidOperationException($"Card with ID {cardId} is already suspended.");
        card.Status = CardStatus.Suspended;
        _unitOfWork.CardRepository.Update(card);
        _unitOfWork.SaveChanges();
        OnCardUpdated(card);
    }

    public void CloseCard(int cardId)
    {
        Card card = FindCard(cardId);
        _unitOfWork.CardRepository.Delete(card);
        _unitOfWork.SaveChanges();
        OnCardClosed(cardId);
    }

    public IEnumerable<Card> ListCardsByAccount(string accountNum)
    {
        return _unitOfWork.CardRepository.Query(x => x.Account.AccountNumber == accountNum, x => x.Account);
    }

    // TODO: Change cardId to cardNumber and adjust logic in services accordingly
    public Card FindCard(int cardId)
    {
        Card card = _unitOfWork.CardRepository.GetById(cardId)
            ?? throw new InvalidOperationException($"Card with ID {cardId} does not exist.");
        if (!card.Activity.IsActive)
            throw new InvalidOperationException($"Card with ID {cardId} no longer exists.");
        return card;
    }

    public async Task IssueNewCardAsync(string cardNumber, int cardType, string cvc, DateTime ExpirationDate, string accountNum, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(cvc))
            throw new ArgumentException("Card number and CVC cannot be null or empty.");
        if (!Enum.IsDefined(typeof(CardType), cardType))
        {
            throw new ArgumentOutOfRangeException("Corresponding value of card type doesn't exist.");
        }
        if (ExpirationDate <= DateTime.UtcNow)
        {
            throw new ArgumentOutOfRangeException("Expiration date must be in the future.");
        }

        Account account = await _accountService.FindAccountAsync(accountNum, cancellationToken);

        Card card = new Card()
        {
            CardNumber = cardNumber,
            CardType = (CardType)cardType,
            CVC = cvc,
            ExpirationDate = ExpirationDate,
            Status = CardStatus.Inactive,
            Account = account,
            Activity = new ActivityInfo()
        };

        await _unitOfWork.CardRepository.InsertAsync(card, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCardIssued(card);
    }

    public async Task ActivateCardAsync(int cardId, CancellationToken cancellationToken)
    {
        Card card = await FindCardAsync(cardId, cancellationToken);
        if (card.Status == CardStatus.Active)
            throw new InvalidOperationException($"Card with ID {cardId} is already active.");
        card.Status = CardStatus.Active;
        await _unitOfWork.CardRepository.UpdateAsync(card);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCardUpdated(card);
    }

    public async Task DeactivateCardAsync(int cardId, CancellationToken cancellationToken)
    {
        Card card = await FindCardAsync(cardId, cancellationToken);
        if (card.Status == CardStatus.Inactive)
            throw new InvalidOperationException($"Card with ID {cardId} is already inactive.");
        card.Status = CardStatus.Inactive;
        await _unitOfWork.CardRepository.UpdateAsync(card);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCardUpdated(card);
    }

    public async Task SuspendCardAsync(int cardId, CancellationToken cancellationToken)
    {
        Card card = await FindCardAsync(cardId, cancellationToken);
        if (card.Status == CardStatus.Suspended)
            throw new InvalidOperationException($"Card with ID {cardId} is already suspended.");
        card.Status = CardStatus.Suspended;
        await _unitOfWork.CardRepository.UpdateAsync(card);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCardUpdated(card);
    }

    public async Task CloseCardAsync(int cardId, CancellationToken cancellationToken)
    {
        Card card = await FindCardAsync(cardId, cancellationToken);
        await _unitOfWork.CardRepository.DeleteAsync(card);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCardClosed(cardId);
    }

    // TODO: Change cardId to cardNumber and adjust logic in services accordingly
    public async Task<Card> FindCardAsync(int cardId, CancellationToken cancellationToken)
    {
        Card card = await _unitOfWork.CardRepository.GetByIdAsync(cardId, cancellationToken)
            ?? throw new InvalidOperationException($"Card with ID {cardId} does not exist.");
        if (!card.Activity.IsActive)
            throw new InvalidOperationException($"Card with ID {cardId} no longer exists.");
        return card;
    }

    public IAsyncEnumerable<Card> ListCardsByAccountAsync(string accountNum, CancellationToken cancellationToken)
    {
        return _unitOfWork.CardRepository.QueryAsync(x => x.Account.AccountNumber == accountNum, cancellationToken, x => x.Account);
    }

    private static void OnCardIssued(Card card)
    {
        CardIssued?.Invoke(card);
    }

    private static void OnCardUpdated(Card card)
    {
        CardUpdated?.Invoke(card);
    }

    private static void OnCardClosed(int cardId)
    {
        CardClosed?.Invoke(cardId);
    }
}
