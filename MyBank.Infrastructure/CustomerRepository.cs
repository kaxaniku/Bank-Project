using MyBank.Domain;
using MyBank.Infrastructure.Interfaces;

namespace MyBank.Infrastructure.Repositories
{
    internal class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BankDbContext context) : base(context) { }
    }
}
