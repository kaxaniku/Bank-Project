using MyBank.Application.Interfaces.Repositories;

namespace MyBank.Infrastructure;

internal class AccountRepository : BaseRepository<Domain.Account>, IAccountRepository
{
    public AccountRepository(BankDbContext context) : base(context) { }
}
