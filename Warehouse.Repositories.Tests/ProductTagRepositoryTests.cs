using Microsoft.Data.SqlClient;
using Warehouse.DTO;

namespace Warehouse.Repositories.Tests;

public class ProductTagRepositoryTests : BaseRepositoryTests<ProductTag>
{
    [Test]
    public void TestInsert_ShouldInsert()
    {
        ProductTagRepository repository = new(_connection!);
        ProductTag productTag = new()
        {
            TagId = 1,
            ProductId = 1
        };

        int id = (int)repository.Insert(productTag);
        ProductTag? result = repository.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(productTag.TagId, result!.TagId);
        Assert.AreEqual(productTag.ProductId, result!.ProductId);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        ProductTagRepository repository = new(_connection!);
        ProductTag productTag = new()
        {
            TagId = 0, // Invalid ID, should fail
            ProductId = 1
        };

        Assert.Throws<SqlException>(() => repository.Insert(productTag));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        ProductTagRepository repository = new(_connection!);
        ProductTag? current = repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        repository.Delete(Constants.DeleteTestId);
        ProductTag? deleted = repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        ProductTagRepository repository = new(_connection!);
        ProductTag? current = repository.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        repository.Delete(Constants.DeleteTestId2);
        ProductTag? deleted = repository.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => repository.Delete(Constants.DeleteTestId2));
    }
}
