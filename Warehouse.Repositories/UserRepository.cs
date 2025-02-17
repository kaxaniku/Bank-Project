using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using Warehouse.DTO;
namespace Warehouse.Repositories
{
    public interface IUserRepository
    {
        User? Get(int id);
        IEnumerable<User> Query();
        int Insert(User value);
        void Update(User value);
        void Delete(User value);
    }

    class UserRepository(IDbConnection connection) : IUserRepository
    {

        private readonly IDbConnection _connection = connection ?? throw new ArgumentNullException(nameof(connection));

        public User? Get(int id)
        {
            var command = GetCommand("sp_GetUser", CommandType.StoredProcedure);
            AddParameter(command, "@UserID", id, DbType.Int32);

            var reader = command.ExecuteReader();
            if (!reader.Read())
                return null;

            return new User
            {
                UserId = reader.GetInt32(0),
                Username = reader.GetString(1),
                Password = (byte[])reader.GetValue(2),
                UserRole = reader.GetByte(3),
                IsActive = reader.GetBoolean(4),
                CreateDate = reader.GetDateTime(5),
                UpdateDate = reader.IsDBNull(6) ? null : reader.GetDateTime(6)
            };
        }

        public IEnumerable<User> Query()
        {
            throw new NotImplementedException();
        }

        public int Insert(User value)
        {
            var command = GetCommand("sp_InsertUser", CommandType.StoredProcedure);

            AddParameter(command, "@Username", value.Username, DbType.String);
            AddParameter(command, "@Password", value.Password, DbType.Byte);
            AddParameter(command, "@UserId", value.UserId, DbType.Int32);

            command.ExecuteNonQuery();
            return value.UserId;
        }

        public void Update(User value)
        {
            var command = GetCommand("sp_UpdateUser", CommandType.StoredProcedure);
            AddParameter(command, "@UserId", value.UserId, DbType.Int32);
            AddParameter(command, "@Username", value.Username, DbType.String);
            AddParameter(command, "@Password", value.Password, DbType.Byte);
            AddParameter(command, "@UserRole", value.UserRole, DbType.Byte);
            command.ExecuteNonQuery();
        }

        public void Delete(User value)
        {
            var command = GetCommand("sp_DeleteUser", CommandType.StoredProcedure);
            AddParameter(command, "@UserId", value.UserId, DbType.Int32);
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
}
