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
    }
}