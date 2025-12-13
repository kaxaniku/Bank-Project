
using System.Data;
using Microsoft.Data.SqlClient;
using MyBank.API.Extensions;
using Serilog;

namespace MyBank.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.ConfigureLogger();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.AddSerilogLogging();

            var app = builder.Build();

            app.UseSerilogRequestLogging();

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
}
