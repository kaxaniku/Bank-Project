using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using NUnit.Framework;

namespace Warehouse.Repositories.Tests;

public abstract class BaseRepositoryTests<T>
{
    private readonly string _connectionString;
    protected SqlConnection? _connection;

    public BaseRepositoryTests()
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration configuration = configurationBuilder.Build();
        _connectionString = configuration.GetConnectionString("WarehouseTestConnection");
    }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _connection = new SqlConnection(_connectionString);
        SeedDatabase();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        ClearDatabase();
        _connection?.Dispose();
    }

    private void ClearDatabase()
    {
        try
        {
            _connection?.Open();
            SqlCommand command = new("EXEC sp_ClearDatabase", _connection);
            command.ExecuteNonQuery();
        }
        finally
        {
            _connection?.Close();
        }
    }

    private void SeedDatabase()
    {
        try
        {
            _connection?.Open();
            SqlCommand command = new("EXEC sp_SeedDatabase", _connection);
            command.ExecuteNonQuery();
        }
        finally
        {
            _connection?.Close();
        }
    }
}