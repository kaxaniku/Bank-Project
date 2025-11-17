using Microsoft.EntityFrameworkCore;
using MyBank.Application.Interfaces.Repositories;
using MyBank.Domain;

namespace MyBank.Tests
{
    public class CityRepositoryTests : BaseRepositoryTests<City>
    {
        private ICityRepository _repository;

        public override void Setup()
        {
            base.Setup();
            _repository = _unitOfWork.CityRepository;
        }

        [Test]
        public void TestInsert_ShouldInsert()
        {
            City city = new()
            {
                Name = "Tbilisi",
                Activity = new ActivityInfo(),
                Country = _unitOfWork.CountryRepository.GetById(1)!
            };

            _repository.Insert(city);
            _unitOfWork.SaveChanges();

            City inserted = _repository.GetById(city.CityId)!;
            Assert.IsNotNull(inserted);
            Assert.That(city.Name, Is.EqualTo(inserted.Name));
            Assert.That(city.Activity, Is.EqualTo(inserted.Activity));
            Assert.That(city.Country, Is.EqualTo(inserted.Country));
        }

        [Test]
        public void TestInsert_ShouldNotInsert()
        {
            City city = new()
            {
                Name = null!,
                Activity = new ActivityInfo(),
                Country = _unitOfWork.CountryRepository.GetById(1)!
            };

            Assert.Throws<DbUpdateException>(() =>
            {
                _repository.Insert(city);
                _unitOfWork.SaveChanges();
            });
        }

        [Test]
        public void TestUpdate_ShouldUpdate()
        {
            City current = _repository.GetById(Constants.UpdateTestId)!;
            Assert.IsNotNull(current);

            current.Name = "UPDCity";
            current.Activity = new ActivityInfo();
            current.Country = _unitOfWork.CountryRepository.GetById(2)!;

            _repository.Update(current);
            _unitOfWork.SaveChanges();

            City updated = _repository.GetById(Constants.UpdateTestId)!;
            Assert.IsNotNull(updated);
            Assert.That(current.Name, Is.EqualTo(updated.Name));
            Assert.That(current.Activity, Is.EqualTo(updated.Activity));
            Assert.That(current.Country, Is.EqualTo(updated.Country));
        }

        [Test]
        public void TestUpdate_ShouldNotUpdate()
        {
            City current = _repository.GetById(Constants.UpdateTestId)!;
            Assert.IsNotNull(current);

            current.Name = null!;
            current.Activity = new ActivityInfo();
            current.Country = _unitOfWork.CountryRepository.GetById(1)!;

            Assert.Throws<DbUpdateException>(() =>
            {
                _repository.Update(current);
                _unitOfWork.SaveChanges();
            });
        }

        [Test]
        public void TestDelete_ShouldDelete()
        {
            City current = _repository.GetById(Constants.DeleteTestId)!;
            Assert.IsNotNull(current, $"Record with {Constants.DeleteTestId} doesn't exists");

            _repository.Delete(current);
            _unitOfWork.SaveChanges();

            City deleted = _repository.GetById(Constants.DeleteTestId)!;
            Assert.That(deleted.Activity.IsActive, Is.EqualTo(false));
        }

        [Test]
        public void TestDelete_ShouldNotDelete()
        {
            City current = _repository.GetById(Constants.DeleteTestId2)!;
            Assert.IsNotNull(current, $"Record with {Constants.DeleteTestId2} doesn't exists");

            _repository.Delete(current);
            _unitOfWork.SaveChanges();

            City deleted = _repository.GetById(Constants.DeleteTestId2)!;
            Assert.That(deleted.Activity.IsActive, Is.EqualTo(false));
            Assert.Throws<DbUpdateConcurrencyException>(() =>
            {
                _repository.Delete(current);
                _unitOfWork.SaveChanges();
            });
        }
    }
}
