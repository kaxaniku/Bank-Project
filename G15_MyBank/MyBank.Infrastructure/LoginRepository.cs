using MyBank.Application.Interfaces.Repositories;

namespace MyBank.Infrastructure;

internal class LoginRepository : BaseRepository<Domain.Login>, ILoginRepository
{
    public LoginRepository(BankDbContext context) : base(context) { }
}
