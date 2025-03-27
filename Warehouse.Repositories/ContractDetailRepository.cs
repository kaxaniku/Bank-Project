using System.Data;
using Warehouse.DTO;
using Warehouse.Services.Interfaces.Repositories;

namespace Warehouse.Repositories;

internal class ContractDetailRepository : BaseRepository<ContractDetail>, IContractDetailRepository
{
    public ContractDetailRepository(IDbConnection connection, Func<IDbTransaction?>? getTransaction) : base(connection, getTransaction)
    {
    }
}
