using MyBank.Domain;
using MyBank.Infrastructure.Interfaces;

namespace MyBank.Infrastructure.Repositories
{
    internal class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(BankDbContext context) : base(context) { }
    }
}
