namespace MyBank.Application.Interfaces.Repositories;

public interface ICustomerRepository : IBaseRepository<Domain.Customer>
{
    IEnumerable<Domain.Customer> ListActive(int pageNumber = 1, int pageSize = 10);
}
