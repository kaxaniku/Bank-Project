using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Warehouse.DTO;
using Warehouse.Services.Interfaces.Repositories;
using Warehouse.Services.Interfaces.Services;
using Warehouse.Services.Models;

namespace Warehouse.Services.Tests
{
    public class ProductServiceTests : BaseServiceTests<ProductService>
    {
        private IProductService? _service;
        [SetUp]
        public void Setup()
        {
            _service = new ProductService(_unitOfWork!);
        }

        [Test]
        public void TestAddCategory_ShouldAddCategory()
        {
            Category category = new()
            {
                Name = "Test Category",
                Description = "Test Description"
            };

            _service!.AddCategory(category);

            Assert.Pass();
        }

        [Test]
        public void TestAddCategory_ShouldNotAddCategory()
        {
            Category category = new()
            {
                Name = null,
                Description = "Test Description"
            };

            Assert.Throws<SqlException>(() => _service!.AddCategory(category));
        }

        [Test]
        public void TestEditCategory_ShouldEditCategory()
        {
            Category? current = _service!.GetCategory(3);
            Assert.IsNotNull(current);

            current!.Name = "Updated " + current.Name;
            current.Description = "Updated " + current.Description;
            _service!.EditCategory(current);
            Assert.Pass();
        }

        [Test]
        public void TestEditCategory_ShouldNotEditCategory()
        {
            Category? current = _service!.GetCategory(4);
            Assert.IsNotNull(current);

            current!.Name = null;
            current.Description = "Updated " + current.Name;
            Assert.Throws<SqlException>(() => _service!.EditCategory(current));
        }

        [Test]
        public void TestDeleteCategory_ShouldDeleteCategory()
        {
            _service!.DeleteCategory(1);
            Assert.Pass();
        }

        [Test]
        public void TestDeleteCategory_ShouldNotDeleteCategory()
        {
            _service!.DeleteCategory(2);
            Assert.Throws<SqlException>(() => _service!.DeleteCategory(2));
        }

        [Test]
        public void TestGetCategories_ShouldReturnCategories()
        {
            IEnumerable<Category> categories = _service!.GetCategories();
            Assert.IsNotNull(categories);
        }

        [Test]
        public void TestGetCategories_ShouldNotReturnCategories()
        {
            IEnumerable<Category> categories = _service!.GetCategories("NonExistentCategory");
            Assert.IsEmpty(categories);
        }

        [Test]
        public void TestGetCategory_ShouldReturnCategory()
        {
            Category? category = _service!.GetCategory(3);
            Assert.IsNotNull(category);
        }

        [Test]
        public void TestGetCategory_ShouldNotReturnCategory()
        {
            Category? category = _service!.GetCategory(0);
            Assert.IsNull(category);
        }

        [Test]
        public void TestAddProduct_ShouldAddProduct()
        {
            Product product = new()
            {
                Name = "Test Product",
                Description = "Test Description",
                CategoryId = 1,
                Barcode = "1234567890123",
                Dimensions = "10x10x10",
            };

            _service!.AddProduct(product);
            Assert.Pass();
        }

        [Test]
        public void TestAddProduct_ShouldNotAddProduct()
        {
            Product product = new()
            {
                Name = "Test Product",
                Description = "Test Description",
                CategoryId = 1,
                Barcode = null,
                Dimensions = "10x10x10",
            };
            Assert.Throws<SqlException>(() => _service!.AddProduct(product));
        }

        [Test]
        public void TestEditProduct_ShouldEditProduct()
        {
            Product? current = _service!.GetProduct(3);
            Assert.IsNotNull(current);

            current!.CategoryId = 3;
            current.Barcode = "TSHIRT20";
            current.Name = current.Name;
            current.Description = "Updated " + current.Description;
            current.Dimensions = "20x20x20";
            current.Weight = 5.0f;
            _service!.EditProduct(current);
            Assert.Pass();
        }

        [Test]
        public void TestEditProduct_ShouldNotEditProduct()
        {
            Product? current = _service!.GetProduct(3);
            Assert.IsNotNull(current);

            current!.CategoryId = 3;
            current.Barcode = null;
            current.Name = "Updated " + current.Name;
            current.Description = "Updated " + current.Description;
            current.Dimensions = "20x20x20";
            current.Weight = 5.0f;
            Assert.Throws<SqlException>(() => _service!.EditProduct(current));
        }

        [Test]
        public void TestDeleteProduct_ShouldDeleteProduct()
        {
            _service!.DeleteProduct(1);
            Assert.Pass();
        }

        [Test]
        public void TestDeleteProduct_ShouldNotDeleteProduct()
        {
            _service!.DeleteProduct(2);
            Assert.Throws<SqlException>(() => _service!.DeleteProduct(2));
        }

        [Test]
        public void TestGetProduct_shouldReturnProduct()
        {
            Product? product = _service!.GetProduct(3);
            Assert.IsNotNull(product);
        }

        [Test]
        public void TestGetProduct_ShouldNotReturnProduct()
        {
            Product? product = _service!.GetProduct(0);
            Assert.IsNull(product);
        }

        [Test]
        public void TestGetProduct_ShouldReturnProductByBarcode()
        {
            Product? product = _service!.GetProductByBarcode("TSHIRT20");
            Assert.IsNotNull(product);
        }

        [Test]
        public void TestGetProduct_ShouldNotReturnProductByBarcode()
        {
            Product? product = _service!.GetProductByBarcode("NonExistentBarcode");
            Assert.IsNull(product);
        }

        [Test]
        public void TestGetProducts_ShouldReturnProductsByCategory()
        {
            IEnumerable<Product> products = _service!.GetProductsByCategory(1);
            Assert.IsNotEmpty(products);
        }

        [Test]
        public void TestGetProducts_ShouldNotReturnProductsByCategory()
        {
            IEnumerable<Product> products = _service!.GetProductsByCategory(0);
            Assert.IsEmpty(products);
        }

        [Test]
        public void TestGetProducts_ShouldReturnProductsByName()
        {
            IEnumerable<Product> products = _service!.GetProductsByName("T-shirt");
            Assert.IsNotNull(products);
            Assert.IsNotEmpty(products);
        }

        [Test]
        public void TestGetProducts_ShouldNotReturnProductsByName()
        {
            IEnumerable<Product> products = _service!.GetProductsByName("NonExistentProduct");
            Assert.IsEmpty(products);
        }

        [Test]
        public void TestGetTransactions_ShouldReturnTransactions()
        {
            IEnumerable<TransactionResponse> transactionResponses = _service!.GetTransactions(2, new DateTime(2025, 1, 1), new DateTime(2026, 4, 5));
            Assert.IsNotNull(transactionResponses);
            foreach (var item in transactionResponses)
            {
                Assert.That(item.ProductId == 2);
            }
        }
    }
}