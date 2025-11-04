namespace MyBank.Infrastructure.Interfaces;

public interface IUnitOfWork : IDisposable
{
    int SaveChanges();
    void BeginTransaction();
    void Commit();
    void Rollback();

    IAccountRepository AccountRepository { get; }
    ICardRepository CardRepository { get; }
    ICityRepository CityRepository { get; }
    ICountryRepository CountryRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    ITransactionRepository TransactionRepository { get; }
}
