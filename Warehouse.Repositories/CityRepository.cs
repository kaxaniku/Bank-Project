using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

public class CityRepository : BaseRepository<City>, ICityRepository
{
    public CityRepository(IDbConnection connection) : base(connection)
    {
    }
}
