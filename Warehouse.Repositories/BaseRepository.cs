using System.Data;
using Warehouse.Repositories.Interfaces;

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
        throw new NotImplementedException();
    }

    public IEnumerable<T> Query()
    {
        throw new NotImplementedException();
    }

    public object Insert(T value)
    {
        var command = GetCommand($"sp_Insert{_entityName}", CommandType.StoredProcedure);

        //AddParameter(command, "@Name", value.Name, DbType.String);
        //AddParameter(command, "@CountryID", value.CountryId, DbType.Int32);
        //AddParameter(command, "@CityID", value.CityId, DbType.Int32, ParameterDirection.Output);
        command.ExecuteNonQuery();

        return 0;
    }

    public void Update(T value)
    {
        throw new NotImplementedException();
    }

    public void Delete(object id)
    {
        throw new NotImplementedException();
    }

    private static void AddParameter(IDbCommand command, string paramName, object? value, DbType type, ParameterDirection direction = ParameterDirection.Input)
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = paramName;
        parameter.Value = value;
        parameter.DbType = type;
        parameter.Direction = direction;

        command.Parameters.Add(parameter);
    }

    private IDbCommand GetCommand(string commandText, CommandType commandType)
    {
        IDbCommand command = _connection.CreateCommand();
        command.CommandText = commandText;
        command.CommandType = commandType;
        return command;
    }
}