using System.Data;
using System.Reflection;
using Dapper;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

public abstract class BaseRepository<T> : IRepository<T>
{
    protected readonly IDbConnection _connection;
    protected readonly string _entityName;

    private IEnumerable<string> InsertIgnoredProperties =>
        new[] { "IsActive", "CreateDate", "UpdateDate", $"{ _entityName }Id" };

    private IEnumerable<string> UpdateIgnoredProperties =>
        new[] { "IsActive", "CreateDate", "UpdateDate" };

    protected BaseRepository(IDbConnection connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        _entityName = typeof(T).Name;
    }

    public T? Get(object id)
    {
        var parameters = new DynamicParameters();
        parameters.Add($"{_entityName}Id", id);

        return _connection.QueryFirstOrDefault<T>(
            $"sp_Get{_entityName}",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<T> Query()
    {
        throw new NotImplementedException();
    }

    public object Insert(T value)
    {
        var parameters = new DynamicParameters();
        SetInsertParameters(value, parameters);
        _connection.Execute($"sp_Insert{_entityName}", parameters, commandType: CommandType.StoredProcedure);

        return parameters.Get<object>($"{_entityName}Id");
    }

    public void Update(T value)
    {
        var parameters = new DynamicParameters();
        SetUpdateParameters(value, parameters);
        _connection.Execute($"sp_Update{_entityName}", parameters, commandType: CommandType.StoredProcedure);
    }

    public void Delete(object id)
    {
        var parameters = new DynamicParameters();
        parameters.Add($"{_entityName}Id", id);

        _connection.Execute($"sp_Delete{_entityName}", parameters, commandType: CommandType.StoredProcedure);
    }

    private void SetInsertParameters(T value, DynamicParameters parameters)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        parameters.Add($"{_entityName}Id", DbType.Int32, direction: ParameterDirection.Output);
        foreach (var property in properties.Where(p => !InsertIgnoredProperties.Contains(p.Name)))
        {
            parameters.Add(property.Name, property.GetValue(value));
        }
    }

    private void SetUpdateParameters(T value, DynamicParameters parameters)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        foreach (var property in properties.Where(p => !UpdateIgnoredProperties.Contains(p.Name)))
        {
            parameters.Add(property.Name, property.GetValue(value));
        }
    }
}

