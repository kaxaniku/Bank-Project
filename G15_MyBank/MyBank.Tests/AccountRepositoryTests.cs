using Microsoft.EntityFrameworkCore;
using MyBank.Application.Interfaces.Repositories;
using MyBank.Domain;

namespace MyBank.Tests
{
    public class AccountRepositoryTests : BaseRepositoryTests<Account>
    {
        private IAccountRepository _repository;

        public override void Setup()
        {
            base.Setup();
            _repository = _unitOfWork.AccountRepository;
        }

        [Test]
        public void TestInsert_ShouldInsert()
        {
            Account account = new()
            {
                AccountNumber = "9991234567890",
                Customer = _unitOfWork.CustomerRepository.GetById(1)!,
                Balance = 1000.00m,
                Activity = new ActivityInfo()
            };

            _repository.Insert(account);
            _unitOfWork.SaveChanges();

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
                AccountNumber = _unitOfWork.AccountRepository.GetById(3)!.AccountNumber,
                Customer = _unitOfWork.AccountRepository.GetById(2)!.Customer,
                Balance = 0.0m,
                Activity = new ActivityInfo()
            };

            Assert.Throws<DbUpdateException>(() =>
            {
                _repository.Insert(account);
                _unitOfWork.SaveChanges();
            });
        }

        [Test]
        public void TestUpdate_ShouldUpdate()
        {
            Account current = _unitOfWork.AccountRepository.GetById(Constants.UpdateTestId)!;
            Assert.IsNotNull(current);

            current.AccountNumber = "UPDATEDACC1";
            current.Customer = _unitOfWork.AccountRepository.GetById(2)!.Customer;
            current.Balance = current.Balance + 1000;

            _repository.Update(current);
            _unitOfWork.SaveChanges();

            Account updated = _unitOfWork.AccountRepository.GetById(Constants.UpdateTestId)!;
            Assert.IsNotNull(updated);
            Assert.That(current.AccountNumber, Is.EqualTo(updated.AccountNumber));
            Assert.That(current.Customer, Is.EqualTo(updated.Customer));
            Assert.That(current.Balance, Is.EqualTo(updated.Balance));

        }

        [Test]
        public void TestUpdate_ShouldNotUpdate()
        {
            Account account = _unitOfWork.AccountRepository.GetById(Constants.UpdateTestId)!;
            Assert.IsNotNull(account);

            account.AccountNumber = null!;

            Assert.Throws<DbUpdateException>(() =>
            {
                _repository.Update(account);
                _unitOfWork.SaveChanges();
            });
        }

        [Test]
        public void TestDelete_ShouldDelete()
        {
            Account account = _unitOfWork.AccountRepository.GetById(Constants.DeleteTestId)!;
            Assert.IsNotNull(account, $"Record with Id {Constants.DeleteTestId} doesn't exists");

            _repository.Delete(account);
            _unitOfWork.SaveChanges();

            Account deleted = _unitOfWork.AccountRepository.GetById(Constants.DeleteTestId)!;
            Assert.That(deleted!.Activity.IsActive, Is.EqualTo(false));

        }

        [Test]
        public void TestDelete_ShouldNotDelete()
        {
            Account account = _unitOfWork.AccountRepository.GetById(Constants.DeleteTestId2)!;
            Assert.IsNotNull(account, $"Record with Id {Constants.DeleteTestId2} doesn't exists");

            _repository.Delete(account);
            _unitOfWork.SaveChanges();

            Account deleted = _unitOfWork.AccountRepository.GetById(Constants.DeleteTestId2)!;
            Assert.That(deleted!.Activity.IsActive, Is.EqualTo(false));

            Assert.Throws<DbUpdateConcurrencyException>(() =>
            {
                _repository.Delete(account);
                _unitOfWork.SaveChanges();
            });
        }
    }
}
