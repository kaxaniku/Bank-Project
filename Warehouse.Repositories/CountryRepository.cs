using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

internal class CountryRepository : BaseRepository<Country>, ICountryRepository
{
    public CountryRepository(IDbConnection connection) : base(connection)
    {
    }
}
