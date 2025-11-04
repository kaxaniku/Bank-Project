using Microsoft.EntityFrameworkCore;
using MyBank.Domain;

namespace MyBank.Infrastructure;
public class BankDbContext : DbContext
{
    public DbSet<Account>? Accounts { get; set; }

    public DbSet<Card>? Cards { get; set; }

    public DbSet<City>? Cities { get; set; }

    public DbSet<Country>? Countries { get; set; }

    public DbSet<Customer>? Customers { get; set; }

    public DbSet<Transaction>? Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=.;Database=KNBank;Integrated Security = true;TrustServerCertificate=true");
    }
}
