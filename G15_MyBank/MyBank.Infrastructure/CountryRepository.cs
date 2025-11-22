using MyBank.Application.Interfaces.Repositories;

namespace MyBank.Infrastructure;

internal class CountryRepository : BaseRepository<Domain.Country>, ICountryRepository
{
    public CountryRepository(BankDbContext context) : base(context) { }
}
