using Microsoft.Data.SqlClient;
using Warehouse.DTO;

namespace Warehouse.Repositories.Tests;

public class ProductRepositoryTests : BaseRepositoryTests<Product>
{
    [Test]
    public void TestInsert_ShouldInsert()
    {
        ProductRepository repository = new(_connection!);
        Product product = new()
        {
            CategoryId = 1,
            Barcode = "123456789",
            Name = "Test Product",
            Description = "Test Description",
            Dimensions = "10x10x10",
            Weight = 2.5f
        };

        int id = (int)repository.Insert(product);
        Product? result = repository.Get(id);

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
        ProductRepository repository = new(_connection!);
        Product product = new()
        {
            CategoryId = 1,
            Barcode = null, // Required field, should fail
            Name = "Test Product",
            Description = "Test Description",
            Dimensions = "10x10x10",
            Weight = 2.5f
        };

        Assert.Throws<SqlException>(() => repository.Insert(product));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        ProductRepository repository = new(_connection!);
        Product? current = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.CategoryId = 2;
        current.Barcode = "987654321";
        current.Name = "Updated " + current.Name;
        current.Description = "Updated " + current.Description;
        current.Dimensions = "20x20x20";
        current.Weight = 5.0f;
        repository.Update(current);

        Product? updated = repository.Get(Constants.UpdateTestId);
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
        ProductRepository repository = new(_connection!);
        Product? current = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Barcode = null; // Invalid update, should fail

        Assert.Throws<SqlException>(() => repository.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        ProductRepository repository = new(_connection!);
        Product? current = repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        repository.Delete(Constants.DeleteTestId);
        Product? deleted = repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        ProductRepository repository = new(_connection!);
        Product? current = repository.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        repository.Delete(Constants.DeleteTestId2);
        Product? deleted = repository.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => repository.Delete(Constants.DeleteTestId2));
    }
}
