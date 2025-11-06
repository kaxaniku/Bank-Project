using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Infrastructure.Interfaces;

namespace MyBank.RepositoryTest;
public class AccountRepositoryTests : BaseRepositoryTests<Account>
{
    private IAccountRepository? _repository;

    public override void SetUp()
    {
        base.SetUp();
        _repository = _unitOfWork!.AccountRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Account account = new()
        {
            AccountNumber = "9991234567890",
            Customer = _unitOfWork!.CustomerRepository.GetById(1)!,
            Balance = 1000.00m,
            Activity = new ActivityInfo(),
        };

        _repository!.Insert(account);
        _unitOfWork!.SaveChanges();

        Account insertedAccount = _repository.GetById(account.AccountId)!;
        Assert.IsNotNull(insertedAccount);
        Assert.That(account.AccountNumber, Is.EqualTo(insertedAccount.AccountNumber));
        Assert.That(account.Customer, Is.EqualTo(insertedAccount.Customer));
        Assert.That(account.Balance, Is.EqualTo(insertedAccount.Balance));
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        Account account = new()
        {
            AccountNumber = _unitOfWork!.AccountRepository.GetById(4)!.AccountNumber,
            Customer = _unitOfWork!.CustomerRepository.GetById(1)!,
            Balance = 0.0m,
            Activity = new ActivityInfo()
        };

        Assert.Throws<DbUpdateException>(() =>
        {
            _repository!.Insert(account);
            _unitOfWork!.SaveChanges();
        });
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        Account account = _repository!.GetById(Constants.UpdateTestID)!;
        Assert.IsNotNull(account);

        account.AccountNumber = "UPDATED12345";
        account.Balance = 2500.75m;
        account.Customer = _unitOfWork!.CustomerRepository.GetById(2)!;

        _repository.Update(account);
        _unitOfWork!.SaveChanges();

        Account updatedAccount = _repository!.GetById(Constants.UpdateTestID)!;
        Assert.IsNotNull(updatedAccount);
        Assert.That(account.AccountNumber, Is.EqualTo(updatedAccount.AccountNumber));
        Assert.That(account.Balance, Is.EqualTo(updatedAccount.Balance));
        Assert.That(account.Customer, Is.EqualTo(updatedAccount.Customer));
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        Account account = _repository!.GetById(Constants.UpdateTestID)!;
        Assert.IsNotNull(account);
        account.AccountNumber = null!;

        Assert.Throws<DbUpdateException>(() =>
        {
            _repository.Update(account);
            _unitOfWork!.SaveChanges();
        });
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Account account = _repository!.GetById(Constants.DeleteTestID)!;
        Assert.IsNotNull(account, $"Record with Id {Constants.DeleteTestID} doesn't exist");

        _repository.Delete(account);
        _unitOfWork!.SaveChanges();

        Account deletedAccount = _repository!.GetById(Constants.DeleteTestID)!;
        Assert.That(deletedAccount.Activity.IsActive, Is.EqualTo(false));
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Account account = _repository!.GetById(Constants.DeleteTestID2)!;
        Assert.IsNotNull(account, $"Record with Id {Constants.DeleteTestID2} doesn't exist");

        _repository.Delete(account);
        _unitOfWork!.SaveChanges();

        Account deletedAccount = _repository!.GetById(Constants.DeleteTestID2)!;
        Assert.That(deletedAccount.Activity.IsActive, Is.EqualTo(false));
        Assert.Throws<DbUpdateConcurrencyException>(() =>
        {
            _repository.Delete(account);
            _unitOfWork!.SaveChanges();
        });
    }
}