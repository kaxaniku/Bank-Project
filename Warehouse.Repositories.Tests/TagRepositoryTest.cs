using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories.Tests;

public class TagRepositoryTests : BaseRepositoryTests<Tag>
{
    private ITagRepository? _repository;

    [SetUp]
    public void Setup()
    {
        _repository = _unitOfWork!.TagRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Tag tag = new()
        {
            Name = "Electronics",
            Description = "Description Test"
        };


        int id = (int)_repository!.Insert(tag);
        Tag? result = _repository!.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(tag.Name, result!.Name);
        Assert.AreEqual(tag.Description, result!.Description);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        Tag tag = new()
        {
            Name = null,
            Description = "Description Test"
        };

        Assert.Throws<SqlException>(() => _repository!.Insert(tag));
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Tag? current = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId} doesn't exist");

        _repository!.Delete(Constants.DeleteTestId);
        Tag? deleted = _repository!.Get(Constants.DeleteTestId);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId} should not be retried");
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Tag? current = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNotNull(current, $"Record with ID {Constants.DeleteTestId2} doesn't exist");

        _repository!.Delete(Constants.DeleteTestId2);
        Tag? deleted = _repository!.Get(Constants.DeleteTestId2);
        Assert.IsNull(deleted, $"Record with ID {Constants.DeleteTestId2} should not be retried");

        Assert.Throws<SqlException>(() => _repository!.Delete(Constants.DeleteTestId2));
    }
}
