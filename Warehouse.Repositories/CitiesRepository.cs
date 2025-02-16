using System.Data;
using Warehouse.DTO;

namespace Warehouse.Repositories;

public interface ICityRepository
{
    City? Get(int id);
    IEnumerable<City> Query();
    int Insert(City value);
    void Update(City value);
    void Delete(int id);
}

public class CityCategory(IDbConnection connection) : ICityRepository
{
    private readonly IDbConnection _connection = connection ?? throw new ArgumentNullException(nameof(connection));

    public City? Get(int id)
    {
        var command = GetCommand("sp_GetCity", CommandType.StoredProcedure);
        AddParameter(command, "@CityID", id, DbType.Int32);

        var reader = command.ExecuteReader();
        if (!reader.Read()) 
            return null;

        return new City
        {
            CityId = reader.GetInt32(0),
            CountryId = reader.GetInt32(1),
            Name = reader.GetString(2),
            PostCode = reader.GetString(3),
            IsActive = reader.GetBoolean(4),
            CreateDate = reader.GetDateTime(5),
            UpdateDate = reader.IsDBNull(6) ? null : reader.GetDateTime(6)
        };
    }

    public IEnumerable<City> Query()
    {
        throw new NotImplementedException();
    }

    public int Insert(City value)
    {
        var command = GetCommand("sp_InsertCity", CommandType.StoredProcedure);

        AddParameter(command, "@Name", value.Name, DbType.String);
        AddParameter(command, "@CountryID", value.CountryId, DbType.Int32);
        AddParameter(command, "@CityID", value.CityId, DbType.Int32, ParameterDirection.Output);
        command.ExecuteNonQuery();

        return 0;
    }

    public void Update(City value)
    {
        var command = GetCommand("sp_UpdateCity", CommandType.StoredProcedure);
        AddParameter(command, "@CityID", value.CityId, DbType.Int32);
        AddParameter(command, "@CountryID", value.CountryId, DbType.Int32);
        AddParameter(command, "@Name", value.Name, DbType.String);
        AddParameter(command, "@PostCode", value.PostCode, DbType.String);
        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        var command = GetCommand("sp_DeleteCity", CommandType.StoredProcedure);
        AddParameter(command, "@CityID", id, DbType.Int32);
        command.ExecuteNonQuery();
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

    public IDbCommand GetCommand(string commandText, CommandType commandType)
    {
        IDbCommand command = _connection.CreateCommand();
        command.CommandText = commandText;
        command.CommandType = commandType;
        return command;
    }
}