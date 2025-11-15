using MyBank.Domain;
using MyBank.Infrastructure.Interfaces;

namespace MyBank.Infrastructure.Repositories;
internal class CountryRepository : BaseRepository<Country>, ICountryRepository
{
    public CountryRepository(BankDbContext context) : base(context) { }
}
