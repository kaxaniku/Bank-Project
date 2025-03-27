using Warehouse.DTO;

namespace Warehouse.Services.Interfaces.Services;

public interface IProductService
{
    void AddCategory(Category category);
    void EditCategory(Category category);
    void DeleteCategory(int id);
    Category GetCategory(int id);
    IEnumerable<Category> GetCategories();
    void AddProduct(Product product);
    void EditProduct(Product product);
    void DeleteProduct(int id);
    Product GetProduct(int id);
    IEnumerable<Product> GetProducts();
}