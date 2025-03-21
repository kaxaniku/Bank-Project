using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

internal class PositionRepository : BaseRepository<Position>, IPositionRepository
{
    public PositionRepository(IDbConnection connection, Func<IDbTransaction>? transaction) : base(connection, transaction)
    {
    }
}
