using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Application.Interfaces.Repositories;

namespace MyBank.RepositoryTest;

public class CardsRepositoryTests : BaseRepositoryTests
{
    private ICardRepository? _repository;

    public override void SetUp()
    {
        base.SetUp();
        _repository = _unitOfWork!.CardRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Card card = new()
        {
            CardNumber = "1234567890123456",
            CardType = CardType.Visa,
            Status = CardStatus.Active,
            ExpirationDate = DateTime.UtcNow.AddYears(3),
            CVC = "123",
            Account = _unitOfWork!.AccountRepository.GetById(1)!,
            Activity = new ActivityInfo(),
        };

        _repository!.Insert(card);
        _unitOfWork!.SaveChanges();

        Card insertedCard = _repository.GetById(card.CardId)!;
        Assert.IsNotNull(insertedCard);
        Assert.That(card.CardNumber, Is.EqualTo(insertedCard.CardNumber));
        Assert.That(card.CardType, Is.EqualTo(insertedCard.CardType));
        Assert.That(card.Status, Is.EqualTo(insertedCard.Status));
        Assert.That(card.ExpirationDate, Is.EqualTo(insertedCard.ExpirationDate));
        Assert.That(card.CVC, Is.EqualTo(insertedCard.CVC));
        Assert.That(card.Account.AccountId, Is.EqualTo(insertedCard.Account.AccountId));
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        Card card = new()
        {
            CardNumber = null!,
            CardType = CardType.MasterCard,
            Status = CardStatus.Inactive,
            ExpirationDate = DateTime.UtcNow.AddYears(1),
            CVC = "321",
            Activity = new ActivityInfo(),
            Account = _unitOfWork!.AccountRepository.GetById(1)!
        };

        Assert.Throws<DbUpdateException>(() =>
        {
            _repository!.Insert(card);
            _unitOfWork!.SaveChanges();
        });
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        Card card = _repository!.GetById(Constants.UpdateTestID)!;
        Assert.IsNotNull(card);

        card.CardNumber = "9999888877776666";
        card.Status = CardStatus.Suspended;
        _repository.Update(card);
        _unitOfWork!.SaveChanges();

        Card updatedCard = _repository!.GetById(Constants.UpdateTestID)!;
        Assert.IsNotNull(updatedCard);
        Assert.That(card.CardNumber, Is.EqualTo(updatedCard.CardNumber));
        Assert.That(card.Status, Is.EqualTo(updatedCard.Status));
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        Card card = _repository!.GetById(Constants.UpdateTestID)!;
        Assert.IsNotNull(card);
        card.CardNumber = null!;

        Assert.Throws<DbUpdateException>(() =>
        {
            _repository.Update(card);
            _unitOfWork!.SaveChanges();
        });
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Card card = _repository!.GetById(Constants.DeleteTestID)!;
        Assert.IsNotNull(card, $"Record with Id {Constants.DeleteTestID} doesn't exist");

        _repository.Delete(card);
        _unitOfWork!.SaveChanges();

        Card deletedCard = _repository!.GetById(Constants.DeleteTestID)!;
        Assert.That(deletedCard.Activity.IsActive, Is.EqualTo(false));
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Card card = _repository!.GetById(Constants.DeleteTestID2)!;
        Assert.IsNotNull(card, $"Record with Id {Constants.DeleteTestID2} doesn't exist");

        _repository.Delete(card);
        _unitOfWork!.SaveChanges();

        Card deletedCard = _repository!.GetById(Constants.DeleteTestID2)!;
        Assert.That(deletedCard.Activity.IsActive, Is.EqualTo(false));
        Assert.Throws<DbUpdateConcurrencyException>(() =>
        {
            _repository.Delete(card);
            _unitOfWork!.SaveChanges();
        });
    }
}