using Microsoft.EntityFrameworkCore;
using Products.API.Extensions;
using Products.API.Middlewares;
using Products.Repositories;
using Products.Services;
using Products.Services.Interfaces.Repositories;
using Products.Services.Interfaces.Services;

namespace Products.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Add services to the container.

        builder.ConfigureLogger();
        builder.Services.AddControllers();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddDbContext<ProductDbContext>(optionsBuilder => optionsBuilder.UseSqlServer(connectionString));
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(Program));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        app.Run();
    }
}