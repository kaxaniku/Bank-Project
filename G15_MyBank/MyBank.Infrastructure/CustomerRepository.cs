using MyBank.Application.Interfaces.Repositories;

namespace MyBank.Infrastructure;

internal class CustomerRepository : BaseRepository<Domain.Customer>, ICustomerRepository
{
    public CustomerRepository(BankDbContext context) : base(context) { }

    public IEnumerable<Domain.Customer> ListActive(int pageNumber = 1, int pageSize = 10)
    {
        int skip = (pageNumber - 1) * pageSize;

        return Query(x => x.Activity.IsActive)
            .Skip(skip)
            .Take(pageSize)
            .ToList();
    }
}
