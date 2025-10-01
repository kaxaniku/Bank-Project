using Products.DTO;

namespace Products.Services.Interfaces.Services
{
    internal interface IProductService
    {
        void AddProduct(Product product);
        void EditProduct(Product product);
        void DeleteProduct(int id);
        Product? GetProduct(int id);
    }
}
