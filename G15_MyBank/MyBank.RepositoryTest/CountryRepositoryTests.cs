using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Application.Interfaces;

namespace MyBank.RepositoryTest;

public class CountryRepositoryTests : BaseRepositoryTests<Country>
{
    private ICountryRepository? _repository;

    public override void SetUp()
    {
        base.SetUp();
        _repository = _unitOfWork!.CountryRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Country country = new()
        {
            Name = "Test Name",
            Code = "SH1",
            Activity = new ActivityInfo()
        };

        _repository!.Insert(country);
        _unitOfWork!.SaveChanges();

        Country insertedCountry = _repository.GetById(country.CountryId)!;
        Assert.IsNotNull(insertedCountry);
        Assert.That(country.Name, Is.EqualTo(insertedCountry.Name));
        Assert.That(country.Code, Is.EqualTo(insertedCountry.Code));
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        Country country = new()
        {
            Name = null!,
            Code = "LE1",
            Activity = new ActivityInfo()
        };

        Assert.Throws<DbUpdateException>(() =>
        {
            _repository!.Insert(country);
            _unitOfWork!.SaveChanges();
        });
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        Country country = _repository!.GetById(Constants.UpdateTestID)!;
        Assert.IsNotNull(country);

        country.Name = "Updated Name";
        country.Code = "UP2";
        _repository.Update(country);
        _unitOfWork!.SaveChanges();

        Country updatedCountry = _repository!.GetById(Constants.UpdateTestID)!;
        Assert.IsNotNull(updatedCountry);
        Assert.That(country.Name, Is.EqualTo(updatedCountry.Name));
        Assert.That(country.Code, Is.EqualTo(updatedCountry.Code));
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        Country country = _repository!.GetById(Constants.UpdateTestID)!;
        Assert.IsNotNull(country);
        country.Name = null!;

        Assert.Throws<DbUpdateException>(() =>
        {
            _repository.Update(country);
            _unitOfWork!.SaveChanges();
        });
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Country country = _repository!.GetById(Constants.DeleteTestID)!;
        Assert.IsNotNull(country, $"Record with Id {Constants.DeleteTestID} doesn't exist");

        _repository.Delete(country);
        _unitOfWork!.SaveChanges();

        Country deletedCountry = _repository!.GetById(Constants.DeleteTestID)!;
        Assert.That(deletedCountry.Activity.IsActive, Is.EqualTo(false));
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Country country = _repository!.GetById(Constants.DeleteTestID2)!;
        Assert.IsNotNull(country, $"Record with Id {Constants.DeleteTestID2} doesn't exist");

        _repository.Delete(country);
        _unitOfWork!.SaveChanges();

        Country deletedCountry = _repository!.GetById(Constants.DeleteTestID2)!;
        Assert.That(deletedCountry.Activity.IsActive, Is.EqualTo(false));
        Assert.Throws<DbUpdateConcurrencyException>(() =>
        {
            _repository.Delete(country);
            _unitOfWork!.SaveChanges();
        });
    }
}