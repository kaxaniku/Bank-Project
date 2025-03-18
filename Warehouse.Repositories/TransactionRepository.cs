using System.Data;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

internal class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(IDbConnection connection, IDbTransaction? transaction) : base(connection, transaction)
    {
    }
}
