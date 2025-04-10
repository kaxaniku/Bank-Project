using Dapper;
using System.Data;
using Warehouse.DTO;
using Warehouse.Services.Interfaces.Repositories;

namespace Warehouse.Repositories;

internal class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IDbConnection connection, Func<IDbTransaction?>? getTransaction) : base(connection, getTransaction)
    {
    }

    public override object Insert(User value)
    {
        var parameters = new DynamicParameters();
        parameters.Add("EmployeeID", value.UserId);
        parameters.Add("Username", value.Username);
        parameters.Add("Password", value.Password);
        parameters.Add("UserRole", value.UserRole);
        parameters.Add($"{_entityName}Id", DbType.Int32, direction: ParameterDirection.Output);

        _connection.Execute($"sp_Insert{_entityName}", parameters, commandType: CommandType.StoredProcedure);

        return parameters.Get<object>($"{_entityName}Id");
    }

    public int LoginUser(string username, string password)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Username", username);
        parameters.Add("Password", password);
        parameters.Add("Id", DbType.Int32, direction: ParameterDirection.ReturnValue);
        _connection.Execute("sp_UserLogin", parameters, commandType: CommandType.StoredProcedure);
        var userId = parameters.Get<int>("Id");
        return userId;
    }
}