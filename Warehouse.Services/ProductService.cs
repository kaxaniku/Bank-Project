using Warehouse.DTO;
using Warehouse.Services.Interfaces.Repositories;
using Warehouse.Services.Interfaces.Services;

namespace Warehouse.Services;

public class ProductService : IProductService
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

    public IEnumerable<Category> GetCategories()
    {
        return _unitOfWork.CategoryRepository.Query(x => x.IsActive);
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

    public IEnumerable<Product> GetProducts()
    {
        return _unitOfWork.ProductRepository.Query(x => x.IsActive);
    }
}