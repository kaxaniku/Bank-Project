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
        throw new NotImplementedException();
    }

    public void DeleteCategory(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Category> GetCategories()
    {
        throw new NotImplementedException();
    }

    public Category GetCategory(int id)
    {
        throw new NotImplementedException();
    }

    public void AddProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public void EditProduct(Product product)
    {
        throw new NotImplementedException();
    }
    public void DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }
    public Product GetProduct(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product> GetProducts()
    {
        throw new NotImplementedException();
    }
}