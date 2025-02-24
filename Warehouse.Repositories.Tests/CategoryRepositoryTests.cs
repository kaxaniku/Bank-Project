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
        CategoryRepository repository = new(_connection);
        Category category = new()
        {
            CategoryId = 5,
            Name = "Updated Category",
            Description = "Test Description was updated"
        };

        repository.Update(category);
    }

    [Test]
    public void TestGet_ShouldGet()
    {
        CategoryRepository repository = new(_connection);
        int id = 6;

        Category? result = repository.Get(id);
        Console.WriteLine(result!.Name);
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        CategoryRepository repository = new(_connection);
        int id = 7;

        repository.Delete(id);
    }
}