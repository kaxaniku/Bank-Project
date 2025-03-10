using Dapper;
using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

public class ProductTagRepository : BaseRepository<ProductTag>, IProductTagRepository
{
    public ProductTagRepository(IDbConnection connection) : base(connection)
    {    
    }
    public override ProductTag? Get(object id)
    {
        var parameters = new DynamicParameters();

        ProductTag productTag = (ProductTag) id;
        parameters.Add($"TagId", productTag.TagId);
        parameters.Add($"ProductId", productTag.ProductId);
        
        return _connection.QueryFirstOrDefault<ProductTag>(
            $"sp_Get{_entityName}",
            parameters,
            commandType: CommandType.StoredProcedure);
    }


    public override object Insert(ProductTag value)
    {
        var parameters = new DynamicParameters();
        parameters.Add($"TagId", value.TagId);
        parameters.Add($"ProductId", value.ProductId);
        _connection.Execute($"sp_Insert{_entityName}", parameters, commandType: CommandType.StoredProcedure);

        return value;
    }

    public override void Delete(object id)
    {
        var parameters = new DynamicParameters();
        ProductTag productTag = (ProductTag)id;
        parameters.Add($"TagId", productTag.TagId);
        parameters.Add($"ProductId", productTag.ProductId);

        _connection.Execute($"sp_Delete{_entityName}", parameters, commandType: CommandType.StoredProcedure);
    }
}
