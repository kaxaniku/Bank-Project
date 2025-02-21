using Microsoft.Data.SqlClient;
using Warehouse.DTO;

namespace Warehouse.Repositories.Tests
{
    public class CategoryRepositoryTests
    {
        private readonly SqlConnection _connection;

        public CategoryRepositoryTests()
        {
            _connection = new SqlConnection("Data Source=.;Database=G15_WarehouseTest;Integrated Security=True;Trust Server Certificate=True;");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _connection.Dispose();
        }

        [Test]
        public void TestInsert_ShouldInsert()
        {
            CategoryRepository repository = new(_connection);
            Category category = new()
            {
                Name = "Test Category",
                Description = "Test Description"
            };

            int id = (int)repository.Insert(category);
            Assert.Greater(id, 0);
        }

        [Test]
        public void TestUpdate_ShouldUpdate()
        {
            CategoryRepository repository = new(_connection);
            Category category = new()
            {
                CategoryId = 5,
                Name = "Updated Category",
                Description = "Test Description was updated"
            };

            repository.Update(category);
        }

        [Test]
        public void TestGet_ShouldGet()
        {
            CategoryRepository repository = new(_connection);
            int id = 6;

            Category? result = repository.Get(id);
            Console.WriteLine(result!.Name);
        }

        [Test]
        public void TestDelete_ShouldDelete()
        {
            CategoryRepository repository = new(_connection);
            int id = 7;

            repository.Delete(id);
        }
    }
}