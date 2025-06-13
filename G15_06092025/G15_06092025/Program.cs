using AutoCenter.dto;
using Microsoft.EntityFrameworkCore;

namespace G15_06092025;
internal class Program
{
    static void Main(string[] args)
    {
        DbContext context = new MyDbContext();
        context.Database.EnsureCreated();
    }
}
public class MyDbContext : DbContext
{
    public DbSet<Car>? Cars { get; set; }
    public DbSet<Employee>? Employees { get; set; }
    public DbSet<Customer>? Customers { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<City>? Cities { get; set; }
    public DbSet<Country>? Countries { get; set; }
    public DbSet<Model>? Models { get; set; }
    public DbSet<Manufacturer>? Manufacturers { get; set; }
    public DbSet<Driver>? Drivers { get; set; }
    public DbSet<Administrator>? Admins { get; set; }
    public DbSet<Mechanic>? Mechanics { get; set; }
    public DbSet<Document>? Documents { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=.;Database=CarRentalDatabase;Integrated Security = true;TrustServerCertificate=true");
    }
}
