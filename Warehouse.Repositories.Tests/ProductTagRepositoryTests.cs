using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories.Tests;

public class ProductTagRepositoryTests : BaseRepositoryTests<ProductTag>
{
    private IProductTagRepository? _repository;

    [SetUp]
    public void Setup()
    {
        _repository = _unitOfWork!.ProductTagRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {
        ProductTag productTag = new()
        {
            TagId = 3,
            ProductId = 2
        };

        _repository!.Insert(productTag);
        ProductTag? result = _repository!.Get(productTag);

        Assert.IsNotNull(result);
        Assert.AreEqual(productTag.TagId, result!.TagId);
        Assert.AreEqual(productTag.ProductId, result!.ProductId);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        ProductTag productTag = new()
        {
            TagId = 0, // Invalid ID, should fail
            ProductId = 1
        };

        Assert.Throws<SqlException>(() => _repository!.Insert(productTag));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        ProductTag productTag = new ProductTag()
        {
            TagId = 2,
            ProductId = 3
        };

        ProductTag? current = _repository!.Get(productTag);
        Assert.IsNotNull(current, $"Record doesn't exist");

        _repository!.Delete(productTag);
        ProductTag? deleted = _repository!.Get(productTag);
        Assert.IsNull(deleted, $"Record should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        ProductTag productTag = new ProductTag()
        {
            TagId = 1,
            ProductId = 2
        };

        ProductTag? current = _repository!.Get(productTag);
        Assert.IsNotNull(current, $"Record doesn't exist");

        _repository!.Delete(productTag);
        ProductTag? deleted = _repository!.Get(productTag);
        Assert.IsNull(deleted, $"Record should not be retried");

        Assert.Throws<SqlException>(() => _repository!.Delete(productTag));
    }
}
