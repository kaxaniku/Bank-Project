using Warehouse.DTO;
using Warehouse.Services.Models;

namespace Warehouse.Services.Interfaces.Services;

public interface IProductService
{
    void AddCategory(Category category);
    void EditCategory(Category category);
    void DeleteCategory(int id);
    IEnumerable<Category> GetCategories(string? name = "");
    Category? GetCategory(int id);

    void AddProduct(Product product);
    void EditProduct(Product product);
    void DeleteProduct(int id);
    Product? GetProduct(int id);
    Product? GetProductByBarcode(string barcode);
    IEnumerable<Product> GetProductsByCategory(int categoryId);
    IEnumerable<Product> GetProductsByName(string? name = "");

    IEnumerable<TransactionResponse> GetTransactions(int productId, DateTime? startDate = null, DateTime? endDate = null);
}