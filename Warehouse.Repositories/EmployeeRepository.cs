using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

internal class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(IDbConnection connection) : base(connection)
    {
    }
}
