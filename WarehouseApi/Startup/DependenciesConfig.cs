using Warehouse.Factories;
using Warehouse.Services.Interfaces.Services;

namespace WarehouseApi.Startup;

public static class DependenciesConfig
{
    public static void AddDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerServices();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddScoped<IProductService>(_ => ProductServiceFactory.Create());
    }
}
