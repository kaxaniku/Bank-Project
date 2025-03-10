using Microsoft.Data.SqlClient;
using Warehouse.DTO;

namespace Warehouse.Repositories.Tests;

public class TagRepositoryTests : BaseRepositoryTests<Tag>
{
    [Test]
    public void TestInsert_ShouldInsert()
    {
        TagRepository repository = new(_connection!);
        Tag tag = new()
        {
            Name = "Electronics",
            Description = "Description Test"
        };


        int id = (int)repository.Insert(tag);
        Tag? result = repository.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(tag.Name, result!.Name);
        Assert.AreEqual(tag.Description, result!.Description);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        TagRepository repository = new(_connection!);
        Tag tag = new()
        {
            Name = null,
            Description = "Description Test"
        };

        Assert.Throws<SqlException>(() => repository.Insert(tag));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        TagRepository repository = new(_connection!);
        Tag? current = repository.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        repository.Delete(Constants.DeleteTestId);
        Tag? deleted = repository.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        TagRepository repository = new(_connection!);
        Tag? current = repository.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        repository.Delete(Constants.DeleteTestId2);
        Tag? deleted = repository.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => repository.Delete(Constants.DeleteTestId2));
    }
}
