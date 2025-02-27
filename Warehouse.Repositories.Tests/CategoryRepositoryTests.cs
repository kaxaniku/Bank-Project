using Warehouse.DTO;

namespace Warehouse.Repositories.Tests;

public class CategoryRepositoryTests : BaseRepositoryTests<Category>
{

    [Test]
    public void TestInsert_ShouldInsert()
    {
        CategoryRepository repository = new(_connection!);
        Category category = new()
        {
            Name = "Test Category",
            Description = "Test Description"
        };

        int id = (int)repository.Insert(category);
        Category? result = repository.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(category.Name, result!.Name);
        Assert.AreEqual(category.Description, result!.Description);
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        CategoryRepository repository = new(_connection!);
        Category? current = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(current);

        current!.Name = "Updated " + current.Name;
        current.Description = "Updated " + current.Description;
        repository.Update(current);

        Category? updated = repository.Get(Constants.UpdateTestId);
        Assert.IsNotNull(updated);
        Assert.AreEqual(current.Name, updated!.Name);
        Assert.AreEqual(current.Description, updated!.Description);
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        CategoryRepository repository = new(_connection!);
        Category? current = repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        repository.Delete(Constants.DeleteTestId);
        Category? deleted = repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }
}