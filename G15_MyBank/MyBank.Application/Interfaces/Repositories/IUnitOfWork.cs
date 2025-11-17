namespace MyBank.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        IAccountRepository AccountRepository { get; }
        ICardRepository CardRepository { get; }
        ICityRepository CityRepository { get; }
        ICountryRepository CountryRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        ITransactionRepository TransactionRepository { get; }
    }
}
