using MyBank.Application.Interfaces.Repositories;
using MyBank.Domain;

namespace MyBank.Infrastructure
{
    internal class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(MyBankDbContext context) : base(context) { }
    }
}
