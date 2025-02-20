using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

public class StorageRepository : BaseRepository<Storage>, IStorageRepository
{
    public StorageRepository(IDbConnection connection) : base(connection)
    {
    }
}
