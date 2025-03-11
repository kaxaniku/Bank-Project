using System.Data;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

public interface IUnitOfWork
{
}

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnection _connection;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICityRepository _cityRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IContractRepository contractRepository;
    private readonly IContractDetailRepository _contractDetailRepository;
    private readonly IProductRepository _productRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ISlotRepository _slotRepository;
    private readonly IStorageRepository _storageRepository;


    public UnitOfWork(IDbConnection connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        _categoryRepository = new CategoryRepository(connection);
        _cityRepository = new CityRepository(connection);
        _countryRepository = new CountryRepository(connection);
        _customerRepository = new CustomerRepository(connection);
        _employeeRepository = new EmployeeRepository(connection);
        contractRepository = new ContractRepository(connection);
        _contractDetailRepository = new ContractDetailRepository(connection);
        _productRepository = new ProductRepository(connection);
        _transactionRepository = new TransactionRepository(connection);
        _positionRepository = new PositionRepository(connection);
        _tagRepository = new TagRepository(connection);
        _slotRepository = new SlotRepository(connection);
        _storageRepository = new StorageRepository(connection);
    }

    public ICategoryRepository CategoryRepository => _categoryRepository;
    public ICityRepository CityRepository => _cityRepository;
    public ICountryRepository CountryRepository => _countryRepository;
    public ICustomerRepository CustomerRepository => _customerRepository;
    public IEmployeeRepository EmployeeRepository => _employeeRepository;
    public IContractRepository ContractRepository => contractRepository;
    public IContractDetailRepository ContractDetailRepository => _contractDetailRepository;
    public IProductRepository ProductRepository => _productRepository;
    public ITransactionRepository TransactionRepository => _transactionRepository;
    public IPositionRepository PositionRepository => _positionRepository;
    public ITagRepository TagRepository => _tagRepository;
    public ISlotRepository SlotRepository => _slotRepository;
    public IStorageRepository StorageRepository => _storageRepository;
}