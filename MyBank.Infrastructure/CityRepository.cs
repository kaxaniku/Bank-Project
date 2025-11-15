using MyBank.Domain;
using MyBank.Infrastructure.Interfaces;

namespace MyBank.Infrastructure.Repositories;
internal class CityRepository : BaseRepository<City>, ICityRepository
{
    public CityRepository(BankDbContext context) : base(context) { }
}
