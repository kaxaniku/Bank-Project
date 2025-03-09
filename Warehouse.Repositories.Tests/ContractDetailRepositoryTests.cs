using Microsoft.Data.SqlClient;
using Warehouse.DTO;

namespace Warehouse.Repositories.Tests;

public class ContractDetailRepositoryTests : BaseRepositoryTests<ContractDetail>
{

    [Test]
    public void TestInsert_ShouldInsert()
    {

        ContractDetailRepository repository = new(_connection!);
        ContractDetail contractDetail = new()
        {
            SlotId = 3,
            ContractId = 1,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddMonths(1)
        };

        int id = (int)repository.Insert(contractDetail);
        ContractDetail? result = repository.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(contractDetail.SlotId, result!.SlotId);
        Assert.AreEqual(contractDetail.ContractId, result!.ContractId);
        Assert.AreEqual(contractDetail.StartDate, result!.StartDate);
        Assert.AreEqual(contractDetail.EndDate, result!.EndDate);
    }

    [Test]
    public void TestInsert_ShouldNotInsert()
    {
        ContractDetailRepository repository = new(_connection!);
        ContractDetail contractDetail = new()
        {
            SlotId = -1,
            ContractId = 1,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddMonths(1)
        };

        Assert.Throws<SqlException>(() => repository.Insert(contractDetail));
    }
}