using MyBank.Domain;
using MyBank.Application.Interfaces.Repositories;

namespace MyBank.Infrastructure
{
    internal class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(MyBankDbContext context) : base(context) { }
    }
}
