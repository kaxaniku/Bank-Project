using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Domain.Interfaces;
using MyBank.Infrastructure.EntityConfigurations;

namespace MyBank.Infrastructure;
public class BankDbContext : DbContext
{
    public DbSet<Account>? Accounts { get; set; }
    public DbSet<Card>? Cards { get; set; }
    public DbSet<City>? Cities { get; set; }
    public DbSet<Country>? Countries { get; set; }
    public DbSet<Customer>? Customers { get; set; }
    public DbSet<Transaction>? Transactions { get; set; }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Deleted && entry.Entity is IDisable entity)
            {
                entry.State = EntityState.Modified;
                entity.Activity.IsActive = false;
            }
        }

        return base.SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfig());
        modelBuilder.ApplyConfiguration(new CardConfig());
        modelBuilder.ApplyConfiguration(new CustomerConfig());
        modelBuilder.ApplyConfiguration(new TransactionConfig());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionString);
    }
}
