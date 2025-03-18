using Dapper;
using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

internal class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IDbConnection connection, IDbTransaction? transaction) : base(connection, transaction)
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
}
