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

    public UnitOfWork(IDbConnection connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        _categoryRepository = new CategoryRepository(connection);
    }

    public ICategoryRepository CategoryRepository => _categoryRepository;
}