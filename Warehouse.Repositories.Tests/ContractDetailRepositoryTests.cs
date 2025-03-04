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
            SlotId = 1,
            ContractId = 1,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(1)
        };

        int id = (int)repository.Insert(contractDetail);
        ContractDetail? result = repository.Get(id);

        Assert.Greater(id, 0);
        Assert.IsNotNull(result);
        Assert.AreEqual(contractDetail.SlotId, result!.SlotId);
        Assert.AreEqual(contractDetail.ContractId, result!.ContractId);
        Assert.AreEqual(contractDetail.StartDate.ToString("yyyy-MM-dd HH:mm:ss"), result!.StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
        Assert.AreEqual(contractDetail.EndDate.ToString("yyyy-MM-dd HH:mm:ss"), result!.EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
    }
}