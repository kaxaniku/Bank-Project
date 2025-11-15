using MyBank.Domain;
using MyBank.Infrastructure.Interfaces;

namespace MyBank.Infrastructure.Repositories;

internal class CardRepository : BaseRepository<Card>, ICardRepository
{
    public CardRepository(BankDbContext context) : base(context) { }
}
