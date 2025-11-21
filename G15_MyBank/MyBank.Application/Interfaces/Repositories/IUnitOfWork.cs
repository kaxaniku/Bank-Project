namespace MyBank.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        int SaveChanges();
        Task<int> SavechangesAsync(CancellationToken token);
        void BeginTransaction();
        Task BeginTrasactionAsync(CancellationToken token);
        void CommitTransaction();
        Task CommitTransactionAsync(CancellationToken token);
        void RollbackTransaction();
        Task RollbackTransactionAsyn(CancellationToken token);

        IAccountRepository AccountRepository { get; }
        ICardRepository CardRepository { get; }
        ICityRepository CityRepository { get; }
        ICountryRepository CountryRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        ITransactionRepository TransactionRepository { get; }
    }
}
