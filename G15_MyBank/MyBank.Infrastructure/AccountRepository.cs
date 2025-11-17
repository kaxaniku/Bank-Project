using MyBank.Application.Interfaces.Repositories;
using MyBank.Domain;

namespace MyBank.Infrastructure
{
    internal class AccountRepository : BaseRepository<Account>, IAccountRepository 
    {
        public AccountRepository(MyBankDbContext context) : base(context) { }
    }
}
