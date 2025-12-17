namespace MyBank.Application.Interfaces.Repositories;

public interface ICityRepository : IBaseRepository<Domain.City>
{
    IEnumerable<Domain.City> ListActive(int pageNumber = 1, int pageSize = 10);
}
