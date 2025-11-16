using MyBank.Domain;
using MyBank.Infrastructure.Interfaces;

namespace MyBank.Infrastructure.Repositories
{
    internal sealed class TransactionRepository
        : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BankDbContext context) : base(context) { }
    }
}
