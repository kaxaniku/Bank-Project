using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories.Tests;

public class TransactionTests : BaseRepositoryTests<Category>
{
    private ICategoryRepository? _categoryRepository;
    private IProductRepository? _productRepository;

    [SetUp]
    public void Setup()
    {
        _categoryRepository = _unitOfWork!.CategoryRepository;
        _productRepository = _unitOfWork!.ProductRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {

        Category category = new()
        {
            Name = "Test Category",
            Description = "Test Description"
        };

        Category category1 = new()
        {
            Name = "Xili",
            Description = "SuperFood"
        };

        _connection!.Open();
        _unitOfWork!.BeginTransaction();

        int id = (int)_categoryRepository!.Insert(category);
        int id1 = (int)_categoryRepository!.Insert(category1);

        Product product = new()
        {
            Name = "Xili",
            Description = "SuperFood",
            Barcode = "FASF##%DF",
            Dimensions = "10x10x10",
            CategoryId = id1
        };

        int id2 = (int)_productRepository!.Insert(product);

        Assert.DoesNotThrow(() => _unitOfWork.Commit());
        Category? result = _categoryRepository.Get(id);
        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(category.Name, result!.Name);
        Assert.AreEqual(category.Description, result!.Description);

        Category? result1 = _categoryRepository.Get(id1);
        Assert.Greater(id, 0);
        Assert.IsNotNull(result1);
        Assert.AreEqual(category1.Name, result1!.Name);
        Assert.AreEqual(category1.Description, result1!.Description);

        Product? result2 = _productRepository.Get(id2);
        Assert.Greater(id, 0);
        Assert.IsNotNull(result2);
        Assert.AreEqual(product.Name, result2!.Name);
        Assert.AreEqual(product.Description, result2!.Description);
        Assert.AreEqual(product.Barcode, result2!.Barcode);
        Assert.AreEqual(product.Dimensions, result2!.Dimensions);
        Assert.AreEqual(product.CategoryId, result2!.CategoryId);
        _connection!.Close();
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        Category category = new()
        {
            Name = null,
            Description = "Test Description"
        };

        _connection!.Open();
        _unitOfWork!.BeginTransaction();

        Assert.Throws<SqlException>(() => _categoryRepository!.Insert(category));
        Assert.DoesNotThrow(() => _unitOfWork.Rollback());
        _connection!.Close();
    }
}