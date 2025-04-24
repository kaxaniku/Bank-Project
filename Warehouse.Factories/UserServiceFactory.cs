using Microsoft.Data.SqlClient;
using Warehouse.Repositories;
using Warehouse.Services;
using Warehouse.Services.Interfaces.Repositories;
using Warehouse.Services.Interfaces.Services;

namespace Warehouse.Factories;

public static class UserServiceFactory
{
    public static IUserService Create()
    {
        IUnitOfWork unitOfWork = new UnitOfWork(new SqlConnection(ConfigurationManager.ConnectionString));
        return Create(unitOfWork);
    }

    public static IUserService Create(IUnitOfWork unitOfWork)
    {
        return new UserService(unitOfWork);
    }
}