using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

public class ProductTagRepository : BaseRepository<ProductTag>, IProductTagRepository
{
    public ProductTagRepository(IDbConnection connection) : base(connection)
    {
    }
}
