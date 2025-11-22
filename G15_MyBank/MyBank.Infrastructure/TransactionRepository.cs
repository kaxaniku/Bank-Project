using MyBank.Application.Interfaces.Repositories;

namespace MyBank.Infrastructure;

internal class TransactionRepository : BaseRepository<Domain.Transaction>, ITransactionRepository
{
    public TransactionRepository(BankDbContext context) : base(context) { }
}
