using Warehouse.DTO;
using Warehouse.Services.Interfaces.Repositories;
using Warehouse.Services.Interfaces.Services;
using Warehouse.Services.Models;

namespace Warehouse.Services;

public sealed class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public void AddCategory(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);
        _unitOfWork.CategoryRepository.Insert(category);
    }

    public void EditCategory(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);
        _unitOfWork.CategoryRepository.Update(category);
    }

    public void DeleteCategory(int id)
    {
        _unitOfWork.CategoryRepository.Delete(id);
    }

    public IEnumerable<Category> GetCategories(string? name = "")
    {
        return _unitOfWork.CategoryRepository.Query(x => x.IsActive && x.Name.StartsWith(name ?? ""));
    }

    public Category? GetCategory(int id)
    {
        return _unitOfWork.CategoryRepository.Get(id);
    }

    public void AddProduct(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        _unitOfWork.ProductRepository.Insert(product);
    }

    public void EditProduct(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        _unitOfWork.ProductRepository.Update(product);
    }

    public void DeleteProduct(int id)
    {
        _unitOfWork.ProductRepository.Delete(id);
    }

    public Product? GetProduct(int id)
    {
        return _unitOfWork.ProductRepository.Get(id);
    }

    public Product? GetProductByBarcode(string barcode)
    {
        return _unitOfWork.ProductRepository.Query(x => x.IsActive && x.Barcode.Equals(barcode)).FirstOrDefault();
    }

    public IEnumerable<Product> GetProductsByCategory(int categoryId)
    {
        return _unitOfWork.ProductRepository.Query(x => x.IsActive && x.CategoryId.Equals(categoryId));
    }

    public IEnumerable<Product> GetProductsByName(string? name = "")
    {
        return _unitOfWork.ProductRepository.Query(x => x.IsActive && x.Name.StartsWith(name ?? ""));
    }

    public IEnumerable<TransactionResponse> GetTransactions(int productId, DateTime? startDate = null, DateTime? endDate = null)
    {
        List<TransactionResponse> transactionResponses = new List<TransactionResponse>();
        IEnumerable<Transaction> transaction = _unitOfWork.TransactionRepository.Query(x => x.ProductId.Equals(productId));
        foreach (var item in transaction)
        {
            Contract contract = _unitOfWork.ContractRepository.Get(item.ContractId)!;
            IEnumerable<ContractDetail> contractDetail = _unitOfWork.ContractDetailRepository.Query(x => x.StartDate >= startDate && x.EndDate <= endDate && x.ContractId.Equals(contract.ContractId));
            foreach (var item1 in contractDetail)
            {
                transactionResponses.Add(new TransactionResponse
                (
                    item.TransactionId,
                    item.ContractId,
                    item.EmployeeId,
                    item.ProductId,
                    item.SlotId,
                    item.Quantity,
                    item.TransactionType,
                    item.CreateDate,
                    item.CustomerAgent
                ));
            }
        }

        return transactionResponses;
    }
}