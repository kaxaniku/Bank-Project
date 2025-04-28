using Microsoft.Data.SqlClient;
using Warehouse.Repositories;
using Warehouse.Services;
using Warehouse.Services.Interfaces.Repositories;
using Warehouse.Services.Interfaces.Services;

namespace Warehouse.Factories;

public static class ProductServiceFactory
{
    public static IProductService Create()
    {
        IUnitOfWork unitOfWork = new UnitOfWork(new SqlConnection(ConfigurationManager.ConnectionString));
        return Create(unitOfWork);
    }
    public static IProductService Create(IUnitOfWork unitOfWork)
    {
        return new ProductService(unitOfWork);
    }
}