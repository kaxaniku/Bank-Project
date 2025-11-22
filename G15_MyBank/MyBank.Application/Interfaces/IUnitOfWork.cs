namespace MyBank.Application.Interfaces;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    void BeginTransaction();
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    void Commit();
    Task CommitAsync(CancellationToken cancellationToken);
    void Rollback();
    Task RollbackAsync(CancellationToken cancellationToken);

    IAccountRepository AccountRepository { get; }
    ICardRepository CardRepository { get; }
    ICityRepository CityRepository { get; }
    ICountryRepository CountryRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    ITransactionRepository TransactionRepository { get; }
}
