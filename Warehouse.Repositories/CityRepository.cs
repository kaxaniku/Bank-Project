using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

internal class CityRepository : BaseRepository<City>, ICityRepository
{
    public CityRepository(IDbConnection connection, IDbTransaction? transaction) : base(connection, transaction)
    {
    }
}
