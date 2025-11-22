namespace MyBank.Infrastructure;

internal class TransactionRepository : BaseRepository<Domain.Transaction>, Application.Interfaces.ITransactionRepository
{
    public TransactionRepository(BankDbContext context) : base(context) { }
}
