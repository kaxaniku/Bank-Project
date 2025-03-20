using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories.Tests;

public class ProductRepositoryTests : BaseRepositoryTests<Product>
{
    private IProductRepository? _repository;

    [SetUp]
    public void Setup()
    {
        _repository = _unitOfWork!.ProductRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Product product = new()
        {
            CategoryId = 1,
            Barcode = "123456789",
            Name = "Test Product",
            Description = "Test Description",
            Dimensions = "10x10x10",
            Weight = 2.5f
        };

        int id = (int)_repository!.Insert(product);
        Product? result = _repository!.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(product.CategoryId, result!.CategoryId);
        Assert.AreEqual(product.Barcode, result!.Barcode);
        Assert.AreEqual(product.Name, result!.Name);
        Assert.AreEqual(product.Description, result!.Description);
        Assert.AreEqual(product.Dimensions, result!.Dimensions);
        Assert.AreEqual(product.Weight, result!.Weight);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        Product product = new()
        {
            CategoryId = 1,
            Barcode = null, // Required field, should fail
            Name = "Test Product",
            Description = "Test Description",
            Dimensions = "10x10x10",
            Weight = 2.5f
        };

        Assert.Throws<SqlException>(() => _repository!.Insert(product));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        Product? current = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.CategoryId = 2;
        current.Barcode = "987654321";
        current.Name = "Updated " + current.Name;
        current.Description = "Updated " + current.Description;
        current.Dimensions = "20x20x20";
        current.Weight = 5.0f;
        _repository!.Update(current);

        Product? updated = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.CategoryId, updated!.CategoryId);
        Assert.AreEqual(current.Barcode, updated!.Barcode);
        Assert.AreEqual(current.Name, updated!.Name);
        Assert.AreEqual(current.Description, updated!.Description);
        Assert.AreEqual(current.Dimensions, updated!.Dimensions);
        Assert.AreEqual(current.Weight, updated!.Weight);
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        Product? current = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Barcode = null; // Invalid update, should fail

        Assert.Throws<SqlException>(() => _repository!.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Product? current = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        _repository!.Delete(Constants.DeleteTestId);
        Product? deleted = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Product? current = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        _repository!.Delete(Constants.DeleteTestId2);
        Product? deleted = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => _repository!.Delete(Constants.DeleteTestId2));
    }

    [Test]
    public void TestQuery()
    {
        IEnumerable<Product> products = _repository!.Query(p => p.Weight > 2.0);

        Assert.IsNotNull(products);

        foreach (var product in products)
        {
            Assert.Greater(product.Weight, 2.0, $"Product {product.Name} does not have a weight greater than 2.0.");
        }
    }

}
