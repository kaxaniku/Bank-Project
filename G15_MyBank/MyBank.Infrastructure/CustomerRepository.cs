namespace MyBank.Infrastructure;

internal class CustomerRepository : BaseRepository<Domain.Customer>, Interfaces.ICustomerRepository
{
    public CustomerRepository(BankDbContext context) : base(context) { }
}
