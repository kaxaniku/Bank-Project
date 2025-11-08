namespace MyBank.Infrastructure;

internal class TransactionRepository : BaseRepository<Domain.Transaction>, Interfaces.ITransactionRepository
{
    public TransactionRepository(BankDbContext context) : base(context) { }
}
