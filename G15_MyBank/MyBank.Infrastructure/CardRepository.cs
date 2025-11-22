namespace MyBank.Infrastructure;

internal class CardRepository : BaseRepository<Domain.Card>, Application.Interfaces.ICardRepository
{
    public CardRepository(BankDbContext context) : base(context) { }
}
