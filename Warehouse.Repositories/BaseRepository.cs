using System.Data;
using Warehouse.Repositories.Interfaces;
using Dapper;
using System.Reflection;

namespace Warehouse.Repositories;

public abstract class BaseRepository<T> : IRepository<T>
{
    protected readonly IDbConnection _connection;
    protected readonly string _entityName;
    private static readonly HashSet<string> _allowedEntities = new()
    {
        "Category", "City", "ContractDetail", "Contract", "Country",
        "Customer", "Employee", "Position", "Product", "ProductTag",
        "Slot", "Storage", "Tag", "Transaction", "User"
    };
    private static readonly HashSet<string> _invalidProperties = new() { "IsActive", "CreateDate", "UpdateDate", "TransactionDate" };

    protected BaseRepository(IDbConnection connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));

        string entityName = typeof(T).Name;
        if (!_allowedEntities.Contains(entityName))
        {
            throw new InvalidOperationException($"Unauthorized entity: {entityName}");
        }
        _entityName = entityName;
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

        foreach (var property in properties.Where(p => !_invalidProperties.Contains(p.Name)))
        {
            parameters.Add(property.Name, property.GetValue(value));
        }

        _connection.Execute($"sp_Insert{_entityName}", parameters, commandType: CommandType.StoredProcedure);

        return parameters.Get<object>($"{_entityName}ID"); 
    }

    public void Update(T value)
    {
        var parameters = new DynamicParameters();
        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (var property in properties.Where(p => !_invalidProperties.Contains(p.Name)))
        {
            parameters.Add(property.Name, property.GetValue(value));
        }

        _connection.Execute($"sp_Update{_entityName}", parameters, commandType: CommandType.StoredProcedure);
    }

    public void Delete(object id)
    {
        var parameters = new DynamicParameters();
        parameters.Add($"{_entityName}ID", id);

        _connection.Execute($"sp_Delete{_entityName}", parameters, commandType: CommandType.StoredProcedure);
    }
}

