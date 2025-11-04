namespace MyBank.Infrastructure;

public class AccountRepository : BaseRepository<Domain.Account>, Interfaces.IAccountRepository
{
    public AccountRepository(BankDbContext context) : base(context) { }
}
