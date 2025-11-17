using Microsoft.EntityFrameworkCore;
using MyBank.Application.Interfaces.Repositories;
using MyBank.Domain;
using System.Diagnostics.Contracts;

namespace MyBank.Tests
{
    public class TransactionRepositoryTests : BaseRepositoryTests<Transaction>
    {
        private ITransactionRepository _repository;

        public override void Setup()
        {
            base.Setup();
            _repository = _unitOfWork.TransactionRepository;
        }

        [Test]
        public void TestInsert_ShouldInsert()
        {
            Transaction transaction = new()
            {
                TransactionDate = DateTime.UtcNow,
                Amount = 100,
                Status = TransactionStatus.Completed,
                FromAccount = _unitOfWork.AccountRepository.GetById(1)!,
                ToAccount = _unitOfWork.AccountRepository.GetById(2)!,
            };

            _repository.Insert(transaction);
            _unitOfWork.SaveChanges();

            Transaction insertedTransaction = _repository.GetById(transaction.TransactionId)!;
            Assert.IsNotNull(insertedTransaction);
            Assert.That(transaction.TransactionDate, Is.EqualTo(insertedTransaction.TransactionDate));
            Assert.That(transaction.Amount, Is.EqualTo(insertedTransaction.Amount));
            Assert.That(transaction.FromAccount, Is.EqualTo(insertedTransaction.FromAccount));
            Assert.That(transaction.ToAccount, Is.EqualTo(insertedTransaction.ToAccount));
        }

        [Test]
        public void TestInsert_ShouldNotInsert()
        {
            Transaction transaction = new()
            {
                TransactionDate = DateTime.UtcNow,
                Amount = -3300,
                Status = TransactionStatus.Blocked,
                FromAccount = _unitOfWork.AccountRepository.GetById(1)!,
                ToAccount = _unitOfWork.AccountRepository.GetById(2)!,
            };

            Assert.Throws<DbUpdateException>(() =>
            {
                _repository.Insert(transaction);
                _unitOfWork.SaveChanges();
            });
        }

        [Test]
        public void TestUpdate_ShouldUpdate()
        {
            Transaction current = _unitOfWork.TransactionRepository.GetById(Constants.UpdateTestId)!;
            Assert.IsNotNull(current);

            current.TransactionDate = DateTime.UtcNow.AddYears(1);
            current.Amount = current.Amount + 100;
            current.Status = TransactionStatus.Pending;
            current.FromAccount = _unitOfWork.AccountRepository.GetById(1)!;
            current.ToAccount = _unitOfWork.AccountRepository.GetById(2)!;

            _repository.Update(current);
            _unitOfWork.SaveChanges();

            Transaction updated = _unitOfWork.TransactionRepository.GetById(Constants.UpdateTestId)!;
            Assert.IsNotNull(updated);
            Assert.That(current.TransactionDate, Is.EqualTo(updated.TransactionDate));
            Assert.That(current.Amount, Is.EqualTo(updated.Amount));
            Assert.That(current.FromAccount, Is.EqualTo(updated.FromAccount));
            Assert.That(current.ToAccount, Is.EqualTo(updated.ToAccount));

        }

        [Test]
        public void TestUpdate_ShouldNotUpdate()
        {
            Transaction current = _unitOfWork.TransactionRepository.GetById(Constants.UpdateTestId)!;
            Assert.IsNotNull(current);

            current.TransactionDate = DateTime.Now.AddDays(-10);
            current.Amount = -1000;
            current.Status = TransactionStatus.Blocked;
            current.FromAccount = _unitOfWork.AccountRepository.GetById(1)!;
            current.ToAccount = _unitOfWork.AccountRepository.GetById(2)!;

            Assert.Throws<DbUpdateException>(() =>
            {
                _repository.Update(current);
                _unitOfWork.SaveChanges();
            });
        }
    }
}
