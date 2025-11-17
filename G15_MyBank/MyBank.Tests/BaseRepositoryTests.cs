using MyBank.Application.Interfaces.Repositories;
using MyBank.Infrastructure;

namespace MyBank.Tests
{
    public class BaseRepositoryTests<T>
    {
        private MyBankDbContext _context;
        protected IUnitOfWork _unitOfWork;

        [SetUp]
        public virtual void Setup()
        {
            _context = new MyBankDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWork.Dispose();
            _context.Dispose();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var seeder = new Seeder(new MyBankDbContext());
            seeder.ClearDatabase();
            seeder.SeedDatabase();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            var seeder = new Seeder(new MyBankDbContext());
            seeder.ClearDatabase();
        }
    }
}