namespace MyBank.Infrastructure;

internal class CardRepository : BaseRepository<Domain.Card>, Interfaces.ICardRepository
{
    public CardRepository(BankDbContext context) : base(context) { }
}
