using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories.Tests;

public class TransactionTests : BaseRepositoryTests<Category>
{
    private ICategoryRepository? _repository;

    [SetUp]
    public void Setup()
    {
        _repository = _unitOfWork!.CategoryRepository;
        _repository.SetTransaction(_unitOfWork.Transaction);
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Category category = new()
        {
            Name = "Test Category",
            Description = "Test Description"
        };


        _unitOfWork.BeginTransaction();
        int id = (int)_repository!.Insert(category);

        Assert.DoesNotThrow(() => _unitOfWork.Commit());
        Category? result = _repository.Get(id);
        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(category.Name, result!.Name);
        Assert.AreEqual(category.Description, result!.Description);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        int code;

        Category category = new()
        {
            Name = null,
            Description = "Test Description"
        };
        try
        {
            _repository!.Insert(category);

            code = _unitOfWork.Commit();
        }
        catch
        {
            code = _unitOfWork.Rollback();
        }

        Assert.AreEqual(code, 1);
    }
}