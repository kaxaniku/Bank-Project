using MyBank.Application.Interfaces.Repositories;

namespace MyBank.Infrastructure;

internal class CardRepository : BaseRepository<Domain.Card>, ICardRepository
{
    public CardRepository(BankDbContext context) : base(context) { }
}
