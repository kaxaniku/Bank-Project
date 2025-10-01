using System.Data;
using System.Reflection;
using Dapper;
using Products.DTO;
using Products.Services.Interfaces.Repositories;

namespace Products.Repositories
{
    internal class ProductRepository : IProductRepository
    {
        protected readonly IDbConnection _connection;
        protected ProductRepository(IDbConnection connection, Func<IDbTransaction?>? getTransaction)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public Product? Get(object id)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"ProductId", id);

            return _connection.QueryFirstOrDefault<Product>(
                $"sp_GetProductId",
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
            PropertyInfo[] properties = typeof(Product).GetProperties();
            parameters.Add($"ProductId", DbType.Int32, direction: ParameterDirection.Output);
            foreach (var property in properties)
            {
                parameters.Add(property.Name, property.GetValue(value));
            }
        }

        private void SetUpdateParameters(Product value, DynamicParameters parameters)
        {
            PropertyInfo[] properties = typeof(Product).GetProperties();
            foreach (var property in properties)
            {
                parameters.Add(property.Name, property.GetValue(value));
            }
        }
    }
}
