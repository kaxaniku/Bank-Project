using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Repositories.Interfaces;

namespace Warehouse.Repositories.Tests;

public class ContractDetailRepositoryTests : BaseRepositoryTests<ContractDetail>
{
    private IContractDetailRepository? _repository;

    [SetUp]
    public void Setup()
    {
        _repository = _unitOfWork!.ContractDetailRepository;
    }

    [Test]
    public void TestInsert_ShouldInsert()
    {
        ContractDetail contractDetail = new()
        {
            SlotId = 3,
            ContractId = 1,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddMonths(1)
        };

        int id = (int)_repository!.Insert(contractDetail);
        ContractDetail? result = _repository!.Get(id);

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
    
        ContractDetail contractDetail = new()
        {
            SlotId = -1,
            ContractId = 1,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddMonths(1)
        };

        Assert.Throws<SqlException>(() => _repository!.Insert(contractDetail));
    }

    [Test]
    public void TestQuery()
    {
        IEnumerable<ContractDetail> contractDetails = _repository!.Query(c => c.ContractId > 1 && c.SlotId > 1);

        Assert.IsNotNull(contractDetails);
        Assert.IsNotEmpty(contractDetails);

        foreach (var detail in contractDetails)
        {
            Assert.Greater(detail.ContractId, 1, $"ContractDetail {detail.ContractId} has an invalid ContractId.");
            Assert.Greater(detail.SlotId, 1, $"ContractDetail {detail.ContractId} has an invalid SlotId.");
        }
    }

}