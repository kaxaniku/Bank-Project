using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

internal class ContractRepository : BaseRepository<Contract>, IContractRepository
{
    public ContractRepository(IDbConnection connection, Func<IDbTransaction?>? getTransaction) : base(connection, getTransaction)
    {
    }
}
