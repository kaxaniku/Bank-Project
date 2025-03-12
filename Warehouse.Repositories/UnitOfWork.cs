using System.Data;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

public interface IUnitOfWork
{
}

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnection _connection;
    private readonly Lazy<ICategoryRepository> _categoryRepository;
    private readonly Lazy<ICityRepository> _cityRepository;
    private readonly Lazy<IContractDetailRepository> _contractDetailRepository;
    private readonly Lazy<IContractRepository> _contractRepository;
    private readonly Lazy<ICountryRepository> _countryRepository;
    private readonly Lazy<ICustomerRepository> _customerRepository;
    private readonly Lazy<IEmployeeRepository> _employeeRepository;
    private readonly Lazy<IPositionRepository> _positionRepository;
    private readonly Lazy<IProductRepository> _productRepository;
    private readonly Lazy<IProductTagRepository> _productTagRepository;
    private readonly Lazy<ISlotRepository> _slotRepository;
    private readonly Lazy<IStorageRepository> _storageRepository;
    private readonly Lazy<ITagRepository> _tagRepository;
    private readonly Lazy<ITransactionRepository> _transactionRepository;
    private readonly Lazy<IUserRepository> _userRepository;


    public UnitOfWork(IDbConnection connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(_connection));
        _cityRepository = new Lazy<ICityRepository>(() => new CityRepository(_connection));
        _contractDetailRepository = new Lazy<IContractDetailRepository>(() => new ContractDetailRepository(_connection));
        _contractRepository = new Lazy<IContractRepository>(() => new ContractRepository(_connection));
        _countryRepository = new Lazy<ICountryRepository>(() => new CountryRepository(_connection));
        _customerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(_connection));
        _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(_connection));
        _positionRepository = new Lazy<IPositionRepository>(() => new PositionRepository(_connection));
        _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(_connection));
        _productTagRepository = new Lazy<IProductTagRepository>(() => new ProductTagRepository(_connection));
        _slotRepository = new Lazy<ISlotRepository>(() => new SlotRepository(_connection));
        _storageRepository = new Lazy<IStorageRepository>(() => new StorageRepository(_connection));
        _tagRepository = new Lazy<ITagRepository>(() => new TagRepository(_connection));
        _transactionRepository = new Lazy<ITransactionRepository>(() => new TransactionRepository(_connection));
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_connection));
    }

    public ICategoryRepository CategoryRepository => _categoryRepository.Value;
    public ICityRepository CityRepository => _cityRepository.Value;
    public IContractDetailRepository ContractDetailRepository => _contractDetailRepository.Value;
    public IContractRepository ContractRepository => _contractRepository.Value;
    public ICountryRepository CountryRepository => _countryRepository.Value;
    public ICustomerRepository CustomerRepository => _customerRepository.Value;
    public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;
    public IPositionRepository PositionRepository => _positionRepository.Value;
    public IProductRepository ProductRepository => _productRepository.Value;
    public IProductTagRepository ProductTagRepository => _productTagRepository.Value;
    public ISlotRepository SlotRepository => _slotRepository.Value;
    public IStorageRepository StorageRepository => _storageRepository.Value;
    public ITagRepository TagRepository => _tagRepository.Value;
    public ITransactionRepository TransactionRepository => _transactionRepository.Value;
    public IUserRepository UserRepository => _userRepository.Value;
}