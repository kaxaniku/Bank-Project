using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Services.Interfaces.Repositories;

namespace Warehouse.Repositories.Tests;

public class TransactionRepositoryTests : BaseRepositoryTests<Employee>
{
    private ITransactionRepository? _repository;

    [SetUp]
    public void Setup()
    {
        _repository = _unitOfWork!.TransactionRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {
        Transaction transaction = new()
        {
            ContractId = 1,
            EmployeeId = 5,
            ProductId = 2,
            SlotId = 2,
            Quantity = 10,
            TransactionType = "Import"
        };

        int id = (int)_repository!.Insert(transaction);
        Transaction? result = _repository!.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(transaction.ContractId, result!.ContractId);
        Assert.AreEqual(transaction.EmployeeId, result!.EmployeeId);
        Assert.AreEqual(transaction.ProductId, result!.ProductId);
        Assert.AreEqual(transaction.SlotId, result!.SlotId);
        Assert.AreEqual(transaction.Quantity, result!.Quantity);
        Assert.AreEqual(transaction.TransactionType, result!.TransactionType);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        Transaction transaction = new()
        {
            ContractId = 1,
            EmployeeId = 1,
            ProductId = 1,
            SlotId = 1,
            Quantity = 10,
            TransactionType = "Test"
        };

        Assert.Throws<SqlException>(() => _repository!.Insert(transaction));
    }

    [Test]
    public void TestQuery()
    {
        IEnumerable<Transaction> transactions = _repository!.Query(transaction => transaction.EmployeeId == 5);

        Assert.IsNotNull(transactions);
        Assert.IsNotEmpty(transactions);

        foreach (var transaction in transactions)
        {
            Assert.AreEqual(5, transaction.EmployeeId, $"Transaction with ID {transaction.TransactionId} does not have the expected EmployeeId.");
        }
    }

}