namespace MyBank.Infrastructure;

internal class AccountRepository : BaseRepository<Domain.Account>, Interfaces.IAccountRepository
{
    public AccountRepository(BankDbContext context) : base(context) { }
}
