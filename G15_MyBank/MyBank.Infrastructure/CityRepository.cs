using MyBank.Application.Interfaces.Repositories;

namespace MyBank.Infrastructure;

internal class CityRepository : BaseRepository<Domain.City>, ICityRepository
{
    public CityRepository(BankDbContext context) : base(context) { }
}
