using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories.Tests;

public class CategoryRepositoryTests : BaseRepositoryTests<Category>
{
    public ICategoryRepository Repository => new UnitOfWork(_connection!).CategoryRepository;

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Category category = new()
        {
            Name = "Test Category",
            Description = "Test Description"
        };

        int id = (int)Repository.Insert(category);
        Category? result = Repository.Get(id);

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

        Assert.Throws<SqlException>(() => Repository.Insert(category));
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        Category? current = Repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = "Updated " + current.Name;
        current.Description = "Updated " + current.Description;
        Repository.Update(current);

        Category? updated = Repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.Name, updated!.Name);
        Assert.AreEqual(current.Description, updated!.Description);
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        Category? current = Repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = null;
        current.Description = "Updated " + current.Description;

        Assert.Throws<SqlException>(() => Repository.Update(current));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Category? current = Repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        Repository.Delete(Constants.DeleteTestId);
        Category? deleted = Repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Category? current = Repository.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        Repository.Delete(Constants.DeleteTestId2);
        Category? deleted = Repository.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => Repository.Delete(Constants.DeleteTestId2));
    }
}