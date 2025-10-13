using Products.DTO;

namespace Products.Services.Interfaces.Services;

public interface IProductService
{
    void AddProduct(Product product);
    void EditProduct(Product product);
    void DeleteProduct(int id);
    Product? GetProduct(int id);
}