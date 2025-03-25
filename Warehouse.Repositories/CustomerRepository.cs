using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

internal class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(IDbConnection connection, Func<IDbTransaction?>? getTransaction) : base(connection, getTransaction)
    {
    }
}
