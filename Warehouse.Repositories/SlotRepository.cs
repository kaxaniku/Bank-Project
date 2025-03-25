using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

internal class SlotRepository : BaseRepository<Slot>, ISlotRepository
{
    public SlotRepository(IDbConnection connection, Func<IDbTransaction?>? getTransaction) : base(connection, getTransaction)
    {
    }
}
