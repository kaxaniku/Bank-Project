using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Services.Interfaces.Repositories;
using Warehouse.Services.Interfaces.Services;
using Warehouse.Services.Models;

namespace Warehouse.Services.Tests
{
    public class ProductServiceTests : BaseServiceTests
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
            Category newCategory = new()
            {
                Name = "Test Category",
                Description = "Test Description"
            };

            Assert.DoesNotThrow(() => _service!.AddCategory(newCategory));
            Assert.That(newCategory.CategoryId, Is.GreaterThan(0));

            Category? insertedCategory = _service!.GetCategory(newCategory.CategoryId);
            Assert.That(insertedCategory, Is.Not.Null);
            Assert.That(insertedCategory!.Name, Is.EqualTo(newCategory.Name));
            Assert.That(insertedCategory.Description, Is.EqualTo(newCategory.Description));
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

            Category? updated = _service.GetCategory(current.CategoryId);
            Assert.IsNotNull(updated);
            Assert.That(current.Name, Is.EqualTo(updated!.Name));
            Assert.That(current.Description, Is.EqualTo(updated!.Description));
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
            Category? current = _service!.GetCategory(1);
            Assert.IsNotNull(current);

            Assert.DoesNotThrow(() => _service!.DeleteCategory(current!.CategoryId));
            Category? deleted = _service.GetCategory(current!.CategoryId);
            Assert.IsNull(deleted);
        }

        [Test]
        public void TestDeleteCategory_ShouldNotDeleteCategory()
        {
            Category? current = _service!.GetCategory(2);
            Assert.IsNotNull(current);

            Assert.DoesNotThrow(() => _service!.DeleteCategory(current!.CategoryId));
            Category? deleted = _service.GetCategory(current!.CategoryId);
            Assert.IsNull(deleted);

            Assert.Throws<SqlException>(() => _service!.DeleteCategory(current!.CategoryId));
        }

        [Test]
        public void TestGetCategories_ShouldReturnCategories()
        {
            IEnumerable<Category> categories = _service!.GetCategories();
            Assert.IsNotEmpty(categories);
            foreach (var item in categories)
            {
                Assert.IsNotNull(item);
                Assert.IsNotEmpty(item.Name);
            }
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
            Product newProduct = new()
            {
                Name = "Test Product",
                Description = "Test Description",
                CategoryId = 1,
                Barcode = "1234567890123",
                Dimensions = "10x10x10",
            };

            Assert.DoesNotThrow(() => _service!.AddProduct(newProduct));
            Assert.That(newProduct.ProductId, Is.GreaterThan(0));

            Product? insertedProduct = _service!.GetProduct(newProduct.ProductId);
            Assert.That(insertedProduct, Is.Not.Null);
            Assert.That(insertedProduct!.Name, Is.EqualTo(newProduct.Name));
            Assert.That(insertedProduct.Description, Is.EqualTo(newProduct.Description));
            Assert.That(insertedProduct.CategoryId, Is.EqualTo(newProduct.CategoryId));
            Assert.That(insertedProduct.Barcode, Is.EqualTo(newProduct.Barcode));
            Assert.That(insertedProduct.Dimensions, Is.EqualTo(newProduct.Dimensions));
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

            Product? updated = _service.GetProduct(current.ProductId);
            Assert.IsNotNull(updated);
            Assert.That(current.Name, Is.EqualTo(updated!.Name));
            Assert.That(current.Description, Is.EqualTo(updated!.Description));
            Assert.That(current.Dimensions, Is.EqualTo(updated!.Dimensions));
            Assert.That(current.Weight, Is.EqualTo(updated!.Weight));
            Assert.That(current.Barcode, Is.EqualTo(updated!.Barcode));
            Assert.That(current.CategoryId, Is.EqualTo(updated!.CategoryId));
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
            Product? current = _service!.GetProduct(1);
            Assert.IsNotNull(current);

            Assert.DoesNotThrow(() => _service!.DeleteProduct(current!.ProductId));
            Product? deleted = _service.GetProduct(current!.ProductId);
            Assert.IsNull(deleted);
        }

        [Test]
        public void TestDeleteProduct_ShouldNotDeleteProduct()
        {
            Product? current = _service!.GetProduct(2);
            Assert.IsNotNull(current);

            Assert.DoesNotThrow(() => _service!.DeleteProduct(current!.ProductId));
            Product? deleted = _service.GetProduct(current!.ProductId);
            Assert.IsNull(deleted);
            Assert.Throws<SqlException>(() => _service!.DeleteProduct(current.ProductId));
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
            Assert.That(product!.Barcode, Is.EqualTo("TSHIRT20"));
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
            foreach (var item in products)
            {
                Assert.IsNotNull(item);
                Assert.That(item.CategoryId, Is.EqualTo(1));
            }
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
            Assert.IsNotEmpty(products);
            foreach (var item in products)
            {
                Assert.IsNotNull(item);
                Assert.That(item.Name, Is.EqualTo("T-shirt"));
            }
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
            Assert.IsNotEmpty(transactionResponses);
            foreach (var item in transactionResponses)
            {
                Assert.That(item.ProductId == 2);
            }
        }

        [Test]
        public void TestGetTransactions_ShouldNotReturnTransactions()
        {
            IEnumerable<TransactionResponse> transactionResponses = _service!.GetTransactions(0, new DateTime(2025, 1, 1), new DateTime(2026, 4, 5));
            Assert.IsEmpty(transactionResponses);
        }
    }
}