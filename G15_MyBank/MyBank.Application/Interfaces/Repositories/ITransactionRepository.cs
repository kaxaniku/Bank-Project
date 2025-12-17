namespace MyBank.Application.Interfaces.Repositories;

public interface ITransactionRepository : IBaseRepository<Domain.Transaction>
{
    IEnumerable<Domain.Transaction> ListByType(Domain.TransactionType type, int pageNumber = 1, int pageSize = 10);

    IEnumerable<Domain.Transaction> ListForAccountBetweenDates(
        int accountId,
        DateTime fromDate,
        DateTime toDate,
        int pageNumber = 1,
        int pageSize = 10);
}
