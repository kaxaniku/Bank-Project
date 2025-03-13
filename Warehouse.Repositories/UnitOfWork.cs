using System;
using System.Data;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories;

public interface IUnitOfWork : IDisposable
{
    IDbTransaction Transaction { get; }

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

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnection _connection;
    private IDbTransaction _transaction;
    private bool _disposed;

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
        _connection.Open();

        _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(_connection));
        _countryRepository = new Lazy<ICountryRepository>(() => new CountryRepository(_connection));
        _cityRepository = new Lazy<ICityRepository>(() => new CityRepository(_connection));
        _contractDetailRepository = new Lazy<IContractDetailRepository>(() => new ContractDetailRepository(_connection));
        _contractRepository = new Lazy<IContractRepository>(() => new ContractRepository(_connection));
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

    public IDbTransaction Transaction => _transaction;

    public void BeginTransaction()
    {
        _transaction ??= _connection.BeginTransaction();
    }

    public void Commit()
    {
        try
        {
            _transaction?.Commit();
            _transaction?.Dispose();
            _transaction = null;
        }
        catch
        {
            Rollback();
            throw;
        }
    }

    public void Rollback()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
        _transaction = null;
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _transaction?.Dispose();
            _connection?.Dispose();
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }

    public ICategoryRepository CategoryRepository => _categoryRepository.Value;
    public ICountryRepository CountryRepository => _countryRepository.Value;
    public ICityRepository CityRepository => _cityRepository.Value;
    public IContractDetailRepository ContractDetailRepository => _contractDetailRepository.Value;
    public IContractRepository ContractRepository => _contractRepository.Value;
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
