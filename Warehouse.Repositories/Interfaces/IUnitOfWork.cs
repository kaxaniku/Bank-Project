using System.Data;

namespace Warehouse.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    void Commit();
    void Rollback();

    ICategoryRepository CategoryRepository { get; }
    ICityRepository CityRepository { get; }
    IContractDetailRepository ContractDetailRepository { get; }
    IContractRepository ContractRepository { get; }
    ICountryRepository CountryRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    IPositionRepository PositionRepository { get; }
    IProductRepository ProductRepository { get; }
    IProductTagRepository ProductTagRepository { get; }
    ISlotRepository SlotRepository { get; }
    IStorageRepository StorageRepository { get; }
    ITagRepository TagRepository { get; }
    ITransactionRepository TransactionRepository { get; }
    IUserRepository UserRepository { get; }
}