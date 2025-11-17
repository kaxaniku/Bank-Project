using MyBank.Application.Interfaces.Repositories;
using MyBank.Domain;

namespace MyBank.Infrastructure
{
    internal class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(MyBankDbContext context) : base(context) { }
    }
}
