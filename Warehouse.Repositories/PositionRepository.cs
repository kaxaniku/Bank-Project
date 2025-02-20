using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

public class PositionRepository : BaseRepository<Position>, IPositionRepository
{
    public PositionRepository(IDbConnection connection) : base(connection)
    {
    }
}
