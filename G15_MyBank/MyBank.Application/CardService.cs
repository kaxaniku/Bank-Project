using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;

namespace MyBank.Application;

public sealed class CardService : ICardService
{
    private readonly IUnitOfWork _unitOfWork;

    public CardService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public void IssueNewCard(int accountId)
    {
        throw new NotImplementedException();
    }

    public void ActivateCard(int cardId)
    {
        throw new NotImplementedException();
    }

    public void BlockCard(int cardId)
    {
        throw new NotImplementedException();
    }

    public void UnblockCard(int cardId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<int> ListCardsByAccount(int accountId)
    {
        throw new NotImplementedException();
    }

    public Task IssueNewCardAsync(int accountId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task ActivateCardAsync(int cardId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task BlockCardAsync(int cardId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UnblockCardAsync(int cardId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<int>> ListCardsByAccountAsync(int accountId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
