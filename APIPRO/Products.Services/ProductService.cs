using Products.Services.Interfaces.Services;
using Products.DTO;
using Products.Services.Interfaces.Repositories;

namespace Products.Services
{
    public sealed class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productService)
        {
            _productRepository = productService ?? throw new ArgumentNullException(nameof(productService));
        }
        public void AddProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);
            product.ProductId = (int)_productRepository.Insert(product);

        }
        public void DeleteProduct(int id)
        {
            ArgumentNullException.ThrowIfNull(id);
            _productRepository.Delete(id);
        }
        public Product? GetProduct(int id)
        {
            ArgumentNullException.ThrowIfNull(id);
            return _productRepository.Get(id);
        }
        public void EditProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);
            _productRepository.Update(product);
        }
    }
}
