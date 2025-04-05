using Microsoft.Data.SqlClient;
using Warehouse.Repositories;
using Warehouse.Repositories.Tests;

namespace Warehouse.Services.Tests;

public abstract class BaseServiceTests<T>
{
    private readonly string _connectionString = ConfigurationManager.ConnectionString;
    protected SqlConnection? _connection;
    protected UnitOfWork? _unitOfWork;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _connection = new SqlConnection(_connectionString);
        _unitOfWork = new UnitOfWork(_connection);
        SeedDatabase();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        ClearDatabase();
        _connection?.Dispose();
        _unitOfWork?.Dispose();
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