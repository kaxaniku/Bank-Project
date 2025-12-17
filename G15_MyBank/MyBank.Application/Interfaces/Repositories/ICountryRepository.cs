namespace MyBank.Application.Interfaces.Repositories;

public interface ICountryRepository : IBaseRepository<Domain.Country>
{
    IEnumerable<Domain.Country> ListActive(int pageNumber = 1, int pageSize = 10);
}
