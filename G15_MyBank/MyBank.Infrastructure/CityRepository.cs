using MyBank.Application.Interfaces.Repositories;

namespace MyBank.Infrastructure;

internal class CityRepository : BaseRepository<Domain.City>, ICityRepository
{
    public CityRepository(BankDbContext context) : base(context) { }

    public IEnumerable<Domain.City> ListActive(int pageNumber = 1, int pageSize = 10)
    {
        int skip = (pageNumber - 1) * pageSize;

        return Query(x => x.Activity.IsActive)
            .Skip(skip)
            .Take(pageSize)
            .ToList();
    }
}
