using MyBank.Application.Interfaces.Repositories;
using MyBank.Domain;

namespace MyBank.Infrastructure
{
    internal class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(MyBankDbContext context) : base(context) { }
    }
}
