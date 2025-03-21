using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

internal class StorageRepository : BaseRepository<Storage>, IStorageRepository
{
    public StorageRepository(IDbConnection connection, Func<IDbTransaction>? transaction) : base(connection, transaction)
    {
    }
}
