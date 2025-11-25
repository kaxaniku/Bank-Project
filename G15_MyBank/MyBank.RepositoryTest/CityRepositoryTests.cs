using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Application.Interfaces.Repositories;

namespace MyBank.RepositoryTest;

public class CityRepositoryTests : BaseRepositoryTests
{
    private ICityRepository? _repository;

    public override void SetUp()
    {
        base.SetUp();
        _repository = _unitOfWork!.CityRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {
        City city = new()
        {
            Name = "Test Name",
            Country = _unitOfWork!.CountryRepository.GetById(1)!,
            Activity = new ActivityInfo()
        };

        _repository!.Insert(city);
        _unitOfWork!.SaveChanges();

        City insertedCity = _repository.GetById(city.CityId)!;
        Assert.IsNotNull(insertedCity);
        Assert.That(city.Name, Is.EqualTo(insertedCity.Name));
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        City city = new()
        {
            Name = null!,
            Country = _unitOfWork!.CountryRepository.GetById(0)!,
            Activity = new ActivityInfo()
        };

        Assert.Throws<DbUpdateException>(() =>
        {
            _repository!.Insert(city);
            _unitOfWork.SaveChanges();
        });
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        City city = _repository!.GetById(Constants.UpdateTestID)!;
        Assert.IsNotNull(city);

        city.Name = "Updated Name";
        city.Country = _unitOfWork!.CountryRepository.GetById(1)!;
        _repository.Update(city);
        _unitOfWork!.SaveChanges();

        City updatedCity = _repository!.GetById(Constants.UpdateTestID)!;
        Assert.IsNotNull(updatedCity);
        Assert.That(city.Name, Is.EqualTo(updatedCity.Name));
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        City city = _repository!.GetById(Constants.UpdateTestID)!;
        Assert.IsNotNull(city);
        city.Name = null!;
        city.Country = _unitOfWork!.CountryRepository.GetById(0)!;

        Assert.Throws<DbUpdateException>(() =>
        {
            _repository.Update(city);
            _unitOfWork!.SaveChanges();
        });
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        City city = _repository!.GetById(Constants.DeleteTestID)!;
        Assert.IsNotNull(city, $"Record with Id {Constants.DeleteTestID} doesn't exist");

        _repository.Delete(city);
        _unitOfWork!.SaveChanges();

        City deletedCity = _repository!.GetById(Constants.DeleteTestID)!;
        Assert.That(deletedCity.Activity.IsActive, Is.EqualTo(false));
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        City city = _repository!.GetById(Constants.DeleteTestID2)!;
        Assert.IsNotNull(city, $"Record with Id {Constants.DeleteTestID2} doesn't exist");

        _repository.Delete(city);
        _unitOfWork!.SaveChanges();

        City deletedCity = _repository!.GetById(Constants.DeleteTestID2)!;
        Assert.That(deletedCity.Activity.IsActive, Is.EqualTo(false));
        Assert.Throws<DbUpdateConcurrencyException>(() =>
        {
            _repository.Delete(city);
            _unitOfWork!.SaveChanges();
        });
    }
}