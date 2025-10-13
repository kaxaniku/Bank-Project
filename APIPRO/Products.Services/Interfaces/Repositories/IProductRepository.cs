using Products.DTO;

namespace Products.Services.Interfaces.Repositories;

public interface IProductRepository
{
    Product? Get(object id);
    object Insert(Product value);
    void Update(Product value);
    void Delete(object id);
}