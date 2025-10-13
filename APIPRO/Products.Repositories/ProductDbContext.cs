using Microsoft.EntityFrameworkCore;
using Products.DTO;

namespace Products.Repositories;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; } = null!;
}