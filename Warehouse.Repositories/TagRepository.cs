using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

public class TagRepository : BaseRepository<Tag>, ITagRepository
{
    public TagRepository(IDbConnection connection) : base(connection)
    {
    }
}
