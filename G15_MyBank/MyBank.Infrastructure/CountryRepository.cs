using MyBank.Application.Interfaces.Repositories;

namespace MyBank.Infrastructure;

internal class CountryRepository : BaseRepository<Domain.Country>, ICountryRepository
{
    public CountryRepository(BankDbContext context) : base(context) { }

    public IEnumerable<Domain.Country> ListActive(int pageNumber = 1, int pageSize = 10)
    {
        int skip = (pageNumber - 1) * pageSize;

        return Query(x => x.Activity.IsActive)
            .Skip(skip)
            .Take(pageSize)
            .ToList();
    }
}
