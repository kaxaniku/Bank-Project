namespace MyBank.Infrastructure;

internal class AccountRepository : BaseRepository<Domain.Account>, Application.Interfaces.IAccountRepository
{
    public AccountRepository(BankDbContext context) : base(context) { }
}
