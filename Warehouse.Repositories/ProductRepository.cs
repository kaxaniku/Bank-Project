using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

internal class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(IDbConnection connection, Func<IDbTransaction>? transaction) : base(connection, transaction)
    {
    }
}
