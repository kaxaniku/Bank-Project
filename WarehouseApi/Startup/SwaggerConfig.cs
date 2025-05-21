namespace WarehouseApi.Startup;

public static class SwaggerConfig
{
    public static void AddSwaggerServices(this IServiceCollection services)
    {
        services.AddSwaggerGen();
    }

    public static void ConfigureSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
