namespace MyBank.Infrastructure.Interfaces;

public interface IUnitOfWork : IDisposable
{
    int SaveChanges();
    Task<int> SaveChangesAsync();
    void BeginTransaction();
    Task BeginTransactionAsync();
    void Commit();
    Task CommitAsync();
    void Rollback();
    Task RollbackAsync();

    IAccountRepository AccountRepository { get; }
    ICardRepository CardRepository { get; }
    ICityRepository CityRepository { get; }
    ICountryRepository CountryRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    ITransactionRepository TransactionRepository { get; }
}
