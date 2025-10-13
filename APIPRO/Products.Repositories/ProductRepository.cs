using Products.DTO;
using Products.Services.Interfaces.Repositories;

namespace Products.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Product? Get(object id) => _context.Products.Find(id);

    public object Insert(Product value)
    {
        _context.Products.Add(value);
        _context.SaveChanges();
        return value.ProductId;
    }

    public void Update(Product value)
    {
        _context.Products.Update(value);
        _context.SaveChanges();
    }

    public void Delete(object id)
    {
        var product = _context.Products.Find(id);
        if (product != null) _context.Products.Remove(product);
        _context.SaveChanges();
    }
}