using MyBank.Application.Interfaces.Repositories;
using MyBank.Domain;

namespace MyBank.Infrastructure
{
    internal class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(MyBankDbContext dbContext) : base(dbContext) { }
    }
}
