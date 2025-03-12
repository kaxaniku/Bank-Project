using Microsoft.Data.SqlClient;

namespace Warehouse.Repositories.Tests;

public abstract class BaseRepositoryTests<T>
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