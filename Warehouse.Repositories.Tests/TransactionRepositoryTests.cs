using Microsoft.Data.SqlClient;
using Warehouse.DTO;

namespace Warehouse.Repositories.Tests;

public class TransactionRepositoryTests : BaseRepositoryTests<Employee>
{

    [Test]
    public void TestInsert_ShouldInsert()
    {

        TransactionRepository repository = new(_connection!);
        Transaction transaction = new()
        {
            ContractId = 1,
            EmployeeId = 5,
            ProductId = 2,
            SlotId = 2,
            Quantity = 10,
            TransactionType = "Import"
        };

        int id = (int)repository.Insert(transaction);
        Transaction? result = repository.Get(id);

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
        TransactionRepository repository = new(_connection!);
        Transaction transaction = new()
        {
            ContractId = 1,
            EmployeeId = 1,
            ProductId = 1,
            SlotId = 1,
            Quantity = 10,
            TransactionType = "Test"
        };

        Assert.Throws<SqlException>(() => repository.Insert(transaction));
    }
}