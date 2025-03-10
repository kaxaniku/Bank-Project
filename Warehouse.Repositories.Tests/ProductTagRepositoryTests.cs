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
            TagId = 3,
            ProductId = 2
        };

        repository.Insert(productTag);
        ProductTag? result = repository.Get(productTag);

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
        ProductTag productTag = new ProductTag()
        {
            TagId = 2,
            ProductId = 3
        };

        ProductTag? current = repository.Get(productTag);
        Assert.IsNotNull(current, $"Record doesn't exist");

        repository.Delete(productTag);
        ProductTag? deleted = repository.Get(productTag);
        Assert.IsNull(deleted, $"Record should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        ProductTagRepository repository = new(_connection!);
        ProductTag productTag = new ProductTag()
        {
            TagId = 1,
            ProductId = 2
        };

        ProductTag? current = repository.Get(productTag);
        Assert.IsNotNull(current, $"Record doesn't exist");

        repository.Delete(productTag);
        ProductTag? deleted = repository.Get(productTag);
        Assert.IsNull(deleted, $"Record should not be retried");

        Assert.Throws<SqlException>(() => repository.Delete(productTag));
    }
}
