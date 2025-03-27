using System.Data;
using Warehouse.DTO;
using Warehouse.Services.Interfaces.Repositories;

namespace Warehouse.Repositories;

internal class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(IDbConnection connection, Func<IDbTransaction?>? getTransaction) : base(connection, getTransaction)
    {
    }
}