using System.Data;
using Warehouse.Repositories.Interfaces;
using Dapper;
using System.Reflection;

namespace Warehouse.Repositories;

public abstract class BaseRepository<T> : IRepository<T>
{
    protected readonly IDbConnection _connection;
    protected readonly string _entityName;

    protected BaseRepository(IDbConnection connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        _entityName = typeof(T).Name;
    }

    public T? Get(object id)
    {
        var parameters = new DynamicParameters();
        parameters.Add($"{_entityName}ID", id);
        
        return _connection.QueryFirstOrDefault<T>($"sp_Get{_entityName}", parameters, commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<T> Query()
    {
        throw new NotImplementedException();
    }

    public object Insert(T value)
    {
        var parameters = new DynamicParameters();
        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (PropertyInfo property in properties)
        {
            if (PropertyIsValid(property))
            {
                parameters.Add(property.Name, property.GetValue(value));
            }
        }
        
        _connection.Execute($"sp_Insert{_entityName}", parameters, commandType: CommandType.StoredProcedure);

        return parameters.Get<object>($"{_entityName}ID"); 
    }

    public void Update(T value)
    {
        var parameters = new DynamicParameters();
        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (PropertyInfo property in properties)
        {
            if (PropertyIsValid(property))
            {
                parameters.Add(property.Name, property.GetValue(value));
            }
        }

        _connection.Execute($"sp_Update{_entityName}", parameters, commandType: CommandType.StoredProcedure);
    }

    public void Delete(object id)
    {
        var parameters = new DynamicParameters();
        parameters.Add($"{_entityName}ID", id);

        _connection.Execute($"sp_Delete{_entityName}", parameters, commandType: CommandType.StoredProcedure);
    }

    private static bool PropertyIsValid(PropertyInfo property)
    {
        string[] invalidProperties = { "IsActive", "CreateDate", "UpdateDate", "TransactionDate" };

        foreach (string invalidProperty in invalidProperties)
        {
            if (property.Name == invalidProperty)
            {
                return false;
            }
        }

        return true;
    }
}

