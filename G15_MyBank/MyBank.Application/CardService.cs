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

    public void IssueNewCard(string cardNumber, byte cardType, string cvc, DateTime ExpirationDate, string accountNum)
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

    public void ActivateCard(string cardNum)
    {
        Card card = FindCard(cardNum);
        if (card.Status == CardStatus.Active)
            throw new InvalidOperationException($"Card with number {cardNum} is already active.");
        card.Status = CardStatus.Active;
        _unitOfWork.CardRepository.Update(card);
        _unitOfWork.SaveChanges();
        OnCardUpdated(card);
    }

    public void DeactivateCard(string cardNum)
    {
        Card card = FindCard(cardNum);
        if (card.Status == CardStatus.Inactive)
            throw new InvalidOperationException($"Card with number {cardNum} is already inactive.");
        card.Status = CardStatus.Inactive;
        _unitOfWork.CardRepository.Update(card);
        _unitOfWork.SaveChanges();
        OnCardUpdated(card);
    }

    public void SuspendCard(string cardNum)
    {
        Card card = FindCard(cardNum);
        if (card.Status == CardStatus.Suspended)
            throw new InvalidOperationException($"Card with number {cardNum} is already suspended.");
        card.Status = CardStatus.Suspended;
        _unitOfWork.CardRepository.Update(card);
        _unitOfWork.SaveChanges();
        OnCardUpdated(card);
    }

    public void CloseCard(string cardNum)
    {
        Card card = FindCard(cardNum);
        _unitOfWork.CardRepository.Delete(card);
        _unitOfWork.SaveChanges();
        OnCardClosed(card.CardId);
    }

    public IEnumerable<Card> ListCardsByAccount(string accountNum)
    {
        return _unitOfWork.CardRepository.Query(x => x.Account.AccountNumber == accountNum && x.Activity.IsActive, x => x.Account);
    }

    public Card FindCard(string cardNum)
    {
        Card card = _unitOfWork.CardRepository.Query(x => x.CardNumber == cardNum, x => x.Account).FirstOrDefault()
            ?? throw new InvalidOperationException($"Card with number {cardNum} does not exist.");
        if (!card.Activity.IsActive)
            throw new InvalidOperationException($"Card with number {cardNum} no longer exists.");
        return card;
    }

    public async Task IssueNewCardAsync(string cardNumber, byte cardType, string cvc, DateTime expirationDate, string accountNum, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(cvc))
            throw new ArgumentException("Card number and CVC cannot be null or empty.");
        if (!Enum.IsDefined(typeof(CardType), cardType))
        {
            throw new ArgumentOutOfRangeException("Corresponding value of card type doesn't exist.");
        }
        if (expirationDate <= DateTime.UtcNow)
        {
            throw new ArgumentOutOfRangeException("Expiration date must be in the future.");
        }

        Account account = await _accountService.FindAccountAsync(accountNum, cancellationToken);

        Card card = new Card()
        {
            CardNumber = cardNumber,
            CardType = (CardType)cardType,
            CVC = cvc,
            ExpirationDate = expirationDate,
            Status = CardStatus.Inactive,
            Account = account,
            Activity = new ActivityInfo()
        };

        await _unitOfWork.CardRepository.InsertAsync(card, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCardIssued(card);
    }

    public async Task ActivateCardAsync(string cardNum, CancellationToken cancellationToken)
    {
        Card card = await FindCardAsync(cardNum, cancellationToken);
        if (card.Status == CardStatus.Active)
            throw new InvalidOperationException($"Card with number {cardNum} is already active.");
        card.Status = CardStatus.Active;
        await _unitOfWork.CardRepository.UpdateAsync(card);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCardUpdated(card);
    }

    public async Task DeactivateCardAsync(string cardNum, CancellationToken cancellationToken)
    {
        Card card = await FindCardAsync(cardNum, cancellationToken);
        if (card.Status == CardStatus.Inactive)
            throw new InvalidOperationException($"Card with number {cardNum} is already inactive.");
        card.Status = CardStatus.Inactive;
        await _unitOfWork.CardRepository.UpdateAsync(card);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCardUpdated(card);
    }

    public async Task SuspendCardAsync(string cardNum, CancellationToken cancellationToken)
    {
        Card card = await FindCardAsync(cardNum, cancellationToken);
        if (card.Status == CardStatus.Suspended)
            throw new InvalidOperationException($"Card with number {cardNum} is already suspended.");
        card.Status = CardStatus.Suspended;
        await _unitOfWork.CardRepository.UpdateAsync(card);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCardUpdated(card);
    }

    public async Task CloseCardAsync(string cardNum, CancellationToken cancellationToken)
    {
        Card card = await FindCardAsync(cardNum, cancellationToken);
        await _unitOfWork.CardRepository.DeleteAsync(card);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCardClosed(card.CardId);
    }

    public async Task<Card> FindCardAsync(string cardNum, CancellationToken cancellationToken)
    {
        var cards = await _unitOfWork.CardRepository.QueryAsync(x => x.CardNumber == cardNum, cancellationToken, x => x.Account);
        Card card = cards.FirstOrDefault()
            ?? throw new InvalidOperationException($"Card with Number {cardNum} does not exist.");
        if (!card.Activity.IsActive)
            throw new InvalidOperationException($"Card with Number {cardNum} no longer exists.");
        return card;
    }

    public async Task<IEnumerable<Card>> ListCardsByAccountAsync(string accountNum, CancellationToken cancellationToken)
    {
        return await _unitOfWork.CardRepository.QueryAsync(x => x.Account.AccountNumber == accountNum && x.Activity.IsActive, cancellationToken, x => x.Account);
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
