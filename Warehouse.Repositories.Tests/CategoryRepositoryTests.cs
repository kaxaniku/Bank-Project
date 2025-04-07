using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Services.Interfaces.Repositories;

namespace Warehouse.Repositories.Tests;

public class CategoryRepositoryTests : BaseRepositoryTests<Category>
{
    private ICategoryRepository? _repository;

    [SetUp]
    public void Setup()
    {
        _repository = _unitOfWork!.CategoryRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Category category = new()
        {
            Name = "Test Category",
            Description = "Test Description"
        };

        int id = (int)_repository!.Insert(category);
        Category? result = _repository.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(category.Name, result!.Name);
        Assert.AreEqual(category.Description, result!.Description);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        Category category = new()
        {
            Name = null,
            Description = "Test Description"
        };

        Assert.Throws<SqlException>(() => _repository!.Insert(category));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        Category? current = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = "Updated " + current.Name;
        current.Description = "Updated " + current.Description;
        _repository.Update(current);

        Category? updated = _repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.Name, updated!.Name);
        Assert.AreEqual(current.Description, updated!.Description);
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        Category? current = _repository!.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = null;
        current.Description = "Updated " + current.Description;

        Assert.Throws<SqlException>(() => _repository.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Category? current = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        _repository.Delete(Constants.DeleteTestId);
        Category? deleted = _repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Category? current = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        _repository.Delete(Constants.DeleteTestId2);
        Category? deleted = _repository.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => _repository.Delete(Constants.DeleteTestId2));
    }

    [Test]
    public void TestQuery()
    {
        IEnumerable<Category> categories = _repository!.Query(c => c.IsActive && c.CategoryId > 3);

        Assert.IsNotNull(categories);
        Assert.IsNotEmpty(categories);

        foreach (var category in categories)
        {
            Assert.IsTrue(category.IsActive, $"Category {category.CategoryId} is not active.");
            Assert.Greater(category.CategoryId, 3, $"Category {category.CategoryId} does not have an ID greater than 3.");
        }
    }

}