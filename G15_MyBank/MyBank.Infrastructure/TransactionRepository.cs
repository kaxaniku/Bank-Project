using MyBank.Application.Interfaces.Repositories;

namespace MyBank.Infrastructure;

internal class TransactionRepository : BaseRepository<Domain.Transaction>, ITransactionRepository
{
    public TransactionRepository(BankDbContext context) : base(context) { }

    public IEnumerable<Domain.Transaction> ListByType(Domain.TransactionType type, int pageNumber = 1, int pageSize = 10)
    {
        int skip = (pageNumber - 1) * pageSize;

        return Query(x => x.Type == type)
            .Skip(skip)
            .Take(pageSize)
            .ToList();
    }

    public IEnumerable<Domain.Transaction> ListForAccountBetweenDates(
        int accountId,
        DateTime fromDate,
        DateTime toDate,
        int pageNumber = 1,
        int pageSize = 10)
    {
        int skip = (pageNumber - 1) * pageSize;

        return Query(x =>
                (x.FromAccountId == accountId || x.ToAccountId == accountId) &&
                x.TransactionDate >= fromDate &&
                x.TransactionDate <= toDate)
            .Skip(skip)
            .Take(pageSize)
            .ToList();
    }
}
