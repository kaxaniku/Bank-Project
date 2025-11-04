namespace MyBank.Infrastructure;

public class CustomerRepository : BaseRepository<Domain.Customer>, Interfaces.ICustomerRepository
{
    public CustomerRepository(BankDbContext context) : base(context) { }
}
