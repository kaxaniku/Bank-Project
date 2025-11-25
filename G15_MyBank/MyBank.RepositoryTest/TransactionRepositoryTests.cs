using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Application.Interfaces.Repositories;

namespace MyBank.RepositoryTest;

public class TransactionRepositoryTests : BaseRepositoryTests
{
    private ITransactionRepository? _repository;

    public override void SetUp()
    {
        base.SetUp();
        _repository = _unitOfWork!.TransactionRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Transaction transaction = new()
        {
            TransactionDate = DateTime.UtcNow,
            Amount = 1500.00m,
            Description = "Test transaction",
            Status = TransactionStatus.Pending,
            FromAccount = _unitOfWork!.AccountRepository.GetById(1)!,
            ToAccount = _unitOfWork!.AccountRepository.GetById(2)!,
            FromAccountId = 1,
            ToAccountId = 2
        };

        _repository!.Insert(transaction);
        _unitOfWork!.SaveChanges();

        Transaction insertedTransaction = _repository.GetById(transaction.TransactionId)!;
        Assert.IsNotNull(insertedTransaction);
        Assert.That(transaction.TransactionDate, Is.EqualTo(insertedTransaction.TransactionDate));
        Assert.That(transaction.Amount, Is.EqualTo(insertedTransaction.Amount));
        Assert.That(transaction.Description, Is.EqualTo(insertedTransaction.Description));
        Assert.That(transaction.Status, Is.EqualTo(insertedTransaction.Status));
        Assert.That(transaction.FromAccountId, Is.EqualTo(insertedTransaction.FromAccountId));
        Assert.That(transaction.ToAccountId, Is.EqualTo(insertedTransaction.ToAccountId));
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        Transaction transaction = new()
        {
            TransactionDate = DateTime.UtcNow,
            Amount = -1343,
            Status = TransactionStatus.Pending,
            FromAccount = _unitOfWork!.AccountRepository.GetById(1)!,
            ToAccount = _unitOfWork!.AccountRepository.GetById(2)!,
            FromAccountId = 1,
            ToAccountId = 2
        };

        Assert.Throws<DbUpdateException>(() =>
        {
            _repository!.Insert(transaction);
            _unitOfWork!.SaveChanges();
        });
    }

    [Test]
    public void TestUpdate_ShouldUpdate()
    {
        Transaction transaction = _repository!.GetById(Constants.UpdateTestID)!;
        Assert.IsNotNull(transaction);

        transaction.Amount = 2000.50m;
        transaction.Description = "Updated description";
        transaction.Status = TransactionStatus.Completed;

        _repository.Update(transaction);
        _unitOfWork!.SaveChanges();

        Transaction updatedTransaction = _repository!.GetById(Constants.UpdateTestID)!;
        Assert.IsNotNull(updatedTransaction);
        Assert.That(transaction.Amount, Is.EqualTo(updatedTransaction.Amount));
        Assert.That(transaction.Description, Is.EqualTo(updatedTransaction.Description));
        Assert.That(transaction.Status, Is.EqualTo(updatedTransaction.Status));
    }

    [Test]
    public void TestUpdate_ShouldNotUpdate()
    {
        Transaction transaction = _repository!.GetById(Constants.UpdateTestID)!;
        Assert.IsNotNull(transaction);

        transaction.Amount = -235;

        Assert.Throws<DbUpdateException>(() =>
        {
            _repository.Update(transaction);
            _unitOfWork!.SaveChanges();
        });
    }

    [Test]
    public void TestDelete_ShouldDelete()
    {
        Transaction transaction = _repository!.GetById(Constants.DeleteTestID)!;
        Assert.IsNotNull(transaction, $"Record with Id {Constants.DeleteTestID} doesn't exist");

        _repository.Delete(transaction);
        _unitOfWork!.SaveChanges();

        Transaction deletedTransaction = _repository!.GetById(Constants.DeleteTestID)!;
        Assert.That(deletedTransaction, Is.Null);
    }

    [Test]
    public void TestDelete_ShouldNotDelete()
    {
        Transaction transaction = _repository!.GetById(Constants.DeleteTestID2)!;
        Assert.IsNotNull(transaction, $"Record with Id {Constants.DeleteTestID2} doesn't exist");

        _repository.Delete(transaction);
        _unitOfWork!.SaveChanges();

        Transaction deletedTransaction = _repository!.GetById(Constants.DeleteTestID2)!;
        Assert.That(deletedTransaction, Is.Null);
        Assert.Throws<DbUpdateConcurrencyException>(() =>
        {
            _repository.Delete(transaction);
            _unitOfWork!.SaveChanges();
        });
    }
}