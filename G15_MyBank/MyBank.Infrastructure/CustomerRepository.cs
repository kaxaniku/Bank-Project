namespace MyBank.Infrastructure;

internal class CustomerRepository : BaseRepository<Domain.Customer>, Application.Interfaces.ICustomerRepository
{
    public CustomerRepository(BankDbContext context) : base(context) { }
}
