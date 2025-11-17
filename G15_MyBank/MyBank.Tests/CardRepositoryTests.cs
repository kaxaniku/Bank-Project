using Microsoft.EntityFrameworkCore;
using MyBank.Application.Interfaces.Repositories;
using MyBank.Domain;

namespace MyBank.Tests
{
    public class CardRepositoryTests : BaseRepositoryTests<Card>
    {
        private ICardRepository _repository;

        public override void Setup()
        {
            base.Setup();
            _repository = _unitOfWork.CardRepository;
        }

        [Test]
        public void TestInsert_ShouldInsert() 
        {
            Card card = new()
            {
                CardNumber = "4929123487654321",
                CardType = CardType.Visa,
                Status = CardStatus.Active,
                ExpirationDate = new DateTime(2028, 7, 31),
                CVC = "645",
                Activity = new ActivityInfo(),
                Account = _unitOfWork.AccountRepository.GetById(1)!,
            };

            _repository.Insert(card);
            _unitOfWork.SaveChanges();

            var insertedCard = _repository.GetById(card.CardId);
            Assert.IsNotNull(insertedCard);
            Assert.That(card.CardNumber, Is.EqualTo(insertedCard.CardNumber));
            Assert.That(card.CardType, Is.EqualTo(insertedCard.CardType));
            Assert.That(card.Status, Is.EqualTo(insertedCard.Status));
            Assert.That(card.ExpirationDate, Is.EqualTo(insertedCard.ExpirationDate));
            Assert.That(card.CVC, Is.EqualTo(insertedCard.CVC));
        }

        [Test]
        public void TestInsert_ShouldNotInsert()
        {
            Card card = new()
            {
                CardNumber = null,
                CardType = CardType.AmericanExpress,
                Status = CardStatus.Suspended,
                ExpirationDate = new DateTime(2001, 01, 03),
                CVC = "3141",
                Activity = new ActivityInfo(),
                Account = _unitOfWork.AccountRepository.GetById(1)!
            };

            Assert.Throws<DbUpdateException>(() =>
            {
                _repository.Insert(card);
                _unitOfWork.SaveChanges();
            });
        }

        [Test]
        public void TestUpdate_ShouldUpdate()
        {
            Card current = _unitOfWork.CardRepository.GetById(Constants.UpdateTestId)!;
            Assert.IsNotNull(current);

            current.CardNumber = "UPDATED1234";
            current.CardType = CardType.Visa;
            current.Status = CardStatus.Inactive;
            current.ExpirationDate = new(2001, 02, 03);
            current.CVC = "342";

            _repository.Update(current);
            _unitOfWork.SaveChanges();

            Card updated = _unitOfWork.CardRepository.GetById(Constants.UpdateTestId)!;
            Assert.IsNotNull(updated);
            Assert.That(current.CardNumber, Is.EqualTo(updated.CardNumber));
            Assert.That(current.CardType, Is.EqualTo(updated.CardType));
            Assert.That(current.Status, Is.EqualTo(updated.Status));
            Assert.That(current.ExpirationDate, Is.EqualTo(updated.ExpirationDate));
            Assert.That(current.CVC, Is.EqualTo(updated.CVC));
        }

        [Test]
        public void TestUpdate_ShouldNotUpdate()
        {
            Card current = _unitOfWork.CardRepository.GetById(Constants.UpdateTestId)!;
            Assert.IsNotNull(current);

            current.CardNumber = null;
            current.Status = CardStatus.Suspended;

            Assert.Throws<DbUpdateException>(() =>
            {
                _repository.Update(current);
                _unitOfWork.SaveChanges();
            });
        }

        [Test]
        public void TestDelete_ShouldDelete()
        {
            Card current = _unitOfWork.CardRepository.GetById(Constants.DeleteTestId)!;
            Assert.IsNotNull(current, $"Record with {Constants.DeleteTestId} doesn't exist");

            _repository.Delete(current);
            _unitOfWork.SaveChanges();

            Card deleted = _unitOfWork.CardRepository.GetById(Constants.DeleteTestId)!;
            Assert.That(deleted.Activity.IsActive, Is.EqualTo(false));
        }

        [Test]
        public void TestDelete_ShouldNotDelete() 
        {
            Card current = _unitOfWork.CardRepository.GetById(Constants.DeleteTestId2)!;
            Assert.IsNotNull(current);

            _repository.Delete(current);
            _unitOfWork.SaveChanges();

            Card deleted = _unitOfWork.CardRepository.GetById(Constants.DeleteTestId2)!;
            Assert.That(deleted.Activity.IsActive, Is.EqualTo(false));
            Assert.Throws<DbUpdateConcurrencyException>(() =>
            {
                _repository.Delete(current);
                _unitOfWork.SaveChanges();
            });
        }


    }
}
