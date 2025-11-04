using MyBank.Infrastructure;
using MyBank.Infrastructure.Interfaces;

namespace MyBank.RepositoryTest;
public abstract class BaseRepositoryTests<T>
{
    protected IUnitOfWork? _unitOfWork;
    private BankDbContext _context;

    [SetUp]
    public virtual void SetUp()
    {
        _context = new BankDbContext();
        _unitOfWork = new UnitOfWork(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _unitOfWork?.Dispose();
        _context?.Dispose();
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
