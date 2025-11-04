namespace MyBank.Infrastructure;

public class CardRepository : BaseRepository<Domain.Card>, Interfaces.ICardRepository
{
    public CardRepository(BankDbContext context) : base(context) { }
}
