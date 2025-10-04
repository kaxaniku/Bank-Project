using System.Data;
using System.Reflection;
using Dapper;
using Products.DTO;
using Products.Services.Interfaces.Repositories;

namespace Products.Repositories
{
    public class ProductRepository : IProductRepository
    {
        protected readonly IDbConnection _connection;

        public ProductRepository(IDbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public Product? Get(object id)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"ProductId", id);

            return _connection.QueryFirstOrDefault<Product>(
                $"sp_GetProduct",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public object Insert(Product value)
        {
            var parameters = new DynamicParameters();
            SetInsertParameters(value, parameters);
            _connection.Execute($"sp_InsertProduct", parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<object>($"ProductId");
        }

        public virtual void Update(Product value)
        {
            var parameters = new DynamicParameters();
            SetUpdateParameters(value, parameters);
            _connection.Execute($"sp_UpdateProduct", parameters, commandType: CommandType.StoredProcedure);
        }

        public virtual void Delete(object id)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"ProductId", id);

            _connection.Execute($"sp_DeleteProduct", parameters, commandType: CommandType.StoredProcedure);
        }

        private void SetInsertParameters(Product value, DynamicParameters parameters)
        {
            parameters.Add($"ProductId", value.ProductId, DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add($"ProductName", value.ProductName, DbType.String);
            parameters.Add($"Price", value.Price, DbType.Decimal);
            parameters.Add($"Stock", value.Stock, DbType.Int32);
            parameters.Add($"Photo", value.Photo, DbType.Binary);
        }

        private void SetUpdateParameters(Product value, DynamicParameters parameters)
        {
            parameters.Add($"ProductId", value.ProductId, DbType.Int32);
            parameters.Add($"ProductName", value.ProductName, DbType.String);
            parameters.Add($"Price", value.Price, DbType.Decimal);
            parameters.Add($"Stock", value.Stock, DbType.Int32);
            parameters.Add($"Photo", value.Photo, DbType.Binary);
        }
    }
}
