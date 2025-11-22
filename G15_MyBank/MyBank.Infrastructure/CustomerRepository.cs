using MyBank.Application.Interfaces.Repositories;

namespace MyBank.Infrastructure;

internal class CustomerRepository : BaseRepository<Domain.Customer>, ICustomerRepository
{
    public CustomerRepository(BankDbContext context) : base(context) { }
}
