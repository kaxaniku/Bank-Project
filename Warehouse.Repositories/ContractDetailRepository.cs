using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

internal class ContractDetailRepository : BaseRepository<ContractDetail>, IContractDetailRepository
{
    public ContractDetailRepository(IDbConnection connection, Func<IDbTransaction?>? getTransaction) : base(connection, getTransaction)
    {
    }
}
