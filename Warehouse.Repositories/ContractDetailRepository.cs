using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

public class ContractDetailRepository : BaseRepository<ContractDetail>, IContractDetailRepository
{
    public ContractDetailRepository(IDbConnection connection) : base(connection)
    {
    }
}
