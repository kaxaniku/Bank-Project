using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.Application;

public sealed class CardService : ICardService
{
    private readonly IUnitOfWork _unitOfWork;

    public CardService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public static event Action<Card>? CardIssued;
    public static event Action<Card>? CardUpdated;
    public static event Action<int>? CardClosed;

    public void IssueNewCard(Card card)
    {
        if (card == null)
            throw new ArgumentNullException(nameof(card));
        _unitOfWork.CardRepository.Insert(card);
        _unitOfWork.SaveChanges();
        OnCardIssued(card);
    }

    public void ActivateCard(int cardId)
    {
        Card card = _unitOfWork.CardRepository.GetById(cardId)
            ?? throw new InvalidOperationException($"Card with ID {cardId} does not exist.");
        card.Status = CardStatus.Active;
        _unitOfWork.CardRepository.Update(card);
        _unitOfWork.SaveChanges();
        OnCardUpdated(card);
    }

    public void BlockCard(int cardId)
    {
        Card card = _unitOfWork.CardRepository.GetById(cardId)
            ?? throw new InvalidOperationException($"Card with ID {cardId} does not exist.");
        card.Status = CardStatus.Inactive;
        _unitOfWork.CardRepository.Update(card);
        _unitOfWork.SaveChanges();
        OnCardUpdated(card);
    }

    public void SuspendCard(int cardId)
    {
        Card card = _unitOfWork.CardRepository.GetById(cardId)
            ?? throw new InvalidOperationException($"Card with ID {cardId} does not exist.");
        card.Status = CardStatus.Suspended;
        _unitOfWork.CardRepository.Update(card);
        _unitOfWork.SaveChanges();
        OnCardUpdated(card);
    }

    public void CloseCard(int cardId)
    {
        Card card = _unitOfWork.CardRepository.GetById(cardId)
            ?? throw new InvalidOperationException($"Card with ID {cardId} does not exist.");
        _unitOfWork.CardRepository.Delete(card);
        _unitOfWork.SaveChanges();
        OnCardClosed(cardId);
    }

    public IEnumerable<Card> ListCardsByAccount(int accountId)
    {
        return _unitOfWork.CardRepository.Query(x => x.Account.AccountId == accountId, x => x.Account);
    }

    public Card FindCardById(int cardId)
    {
        Card card = _unitOfWork.CardRepository.GetById(cardId)
            ?? throw new InvalidOperationException($"Card with ID {cardId} does not exist.");
        if (!card.Activity.IsActive)
            throw new InvalidOperationException($"Card with ID {cardId} no longer exists.");
        return card;
    }

    public async Task IssueNewCardAsync(Card card, CancellationToken cancellationToken)
    {
        if (card == null)
            throw new ArgumentNullException(nameof(card));
        await _unitOfWork.CardRepository.InsertAsync(card, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCardIssued(card);
    }

    public async Task ActivateCardAsync(int cardId, CancellationToken cancellationToken)
    {
        Card card = await _unitOfWork.CardRepository.GetByIdAsync(cardId, cancellationToken)
            ?? throw new InvalidOperationException($"Card with ID {cardId} does not exist.");
        card.Status = CardStatus.Active;
        await _unitOfWork.CardRepository.UpdateAsync(card);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCardUpdated(card);
    }

    public async Task BlockCardAsync(int cardId, CancellationToken cancellationToken)
    {
        Card card = await _unitOfWork.CardRepository.GetByIdAsync(cardId, cancellationToken)
            ?? throw new InvalidOperationException($"Card with ID {cardId} does not exist.");
        card.Status = CardStatus.Inactive;
        await _unitOfWork.CardRepository.UpdateAsync(card);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCardUpdated(card);
    }

    public async Task SuspendCardAsync(int cardId, CancellationToken cancellationToken)
    {
        Card card = await _unitOfWork.CardRepository.GetByIdAsync(cardId, cancellationToken)
            ?? throw new InvalidOperationException($"Card with ID {cardId} does not exist.");
        card.Status = CardStatus.Suspended;
        await _unitOfWork.CardRepository.UpdateAsync(card);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCardUpdated(card);
    }

    public async Task CloseCardAsync(int cardId, CancellationToken cancellationToken)
    {
        Card card = await _unitOfWork.CardRepository.GetByIdAsync(cardId, cancellationToken)
            ?? throw new InvalidOperationException($"Card with ID {cardId} does not exist.");
        await _unitOfWork.CardRepository.DeleteAsync(card);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnCardClosed(cardId);
    }

    public async Task<Card> FindCardByIdAsync(int cardId, CancellationToken cancellationToken)
    {
        Card card = await _unitOfWork.CardRepository.GetByIdAsync(cardId, cancellationToken)
            ?? throw new InvalidOperationException($"Card with ID {cardId} does not exist.");
        if (!card.Activity.IsActive)
            throw new InvalidOperationException($"Card with ID {cardId} no longer exists.");
        return card;
    }

    public async Task<IEnumerable<Card>> ListCardsByAccountAsync(int accountId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.CardRepository.QueryAsync(x => x.Account.AccountId == accountId, cancellationToken, x => x.Account);
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
