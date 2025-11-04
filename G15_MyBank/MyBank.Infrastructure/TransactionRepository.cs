namespace MyBank.Infrastructure;

public class TransactionRepository : BaseRepository<Domain.Transaction>, Interfaces.ITransactionRepository
{
    public TransactionRepository(BankDbContext context) : base(context) { }
}
