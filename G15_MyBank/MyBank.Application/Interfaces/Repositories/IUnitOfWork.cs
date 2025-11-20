namespace MyBank.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        int SaveChanges();
        Task<int> SavechangesAsync();
        void BeginTransaction();
        Task BeginTrasactionAsync();
        void CommitTransaction();
        Task CommitTransactionAsync();
        void RollbackTransaction();
        Task RollbackTransactionAsyn();

        IAccountRepository AccountRepository { get; }
        ICardRepository CardRepository { get; }
        ICityRepository CityRepository { get; }
        ICountryRepository CountryRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        ITransactionRepository TransactionRepository { get; }
    }
}
