using MyBank.Infrastructure;
using MyBank.Application.Interfaces.Repositories;

namespace MyBank.RepositoryTest;

public abstract class BaseRepositoryTests
{
    protected IUnitOfWork? _unitOfWork;
    private BankDbContext _context;
    protected CancellationTokenSource _cts;

    [SetUp]
    public virtual void SetUp()
    {
        _context = new BankDbContext();
        _unitOfWork = new UnitOfWork(_context);
        _cts = new CancellationTokenSource();
    }

    [TearDown]
    public void TearDown()
    {
        _unitOfWork?.Dispose();
        _context?.Dispose();
        _cts.Dispose();
    }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var seeder = new Seeder(new BankDbContext());
        seeder.ClearDatabase();
        seeder.SeedDatabase();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        var seeder = new Seeder(new BankDbContext());
        seeder.ClearDatabase();
    }
}
