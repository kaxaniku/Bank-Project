using BankSystem.Domain.Entities;

namespace BankSystem.Application.Common.Interfaces.Repositories;

public interface ITransactionRepository : IRepositoryBase<Transaction>
{
    Task<IEnumerable<Transaction>> GetAccountTransactionsAsync(
        int accountId,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        CancellationToken cancellationToken = default);

    Task<Transaction?> GetByReferenceAsync(string reference, CancellationToken cancellationToken = default);

    Task<decimal> GetTotalIncomingAmountAsync(
        int accountId,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        CancellationToken cancellationToken = default);

    Task<decimal> GetTotalOutgoingAmountAsync(
        int accountId,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        CancellationToken cancellationToken = default);
}