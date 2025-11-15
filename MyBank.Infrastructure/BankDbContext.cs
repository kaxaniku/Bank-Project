using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Domain.Interfaces;

namespace MyBank.Infrastructure;
public sealed class BankDbContext : DbContext
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
            if (entry.State != EntityState.Deleted || entry.Entity is not IDisable disableEntity) continue;
            
            entry.State = EntityState.Modified;
            disableEntity.Activity.IsActive = false;
        }

        return base.SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(BankDbContext).Assembly,
            type => type.Namespace == "MyBank.Infrastructure.EntityConfigurations"
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionString);
    }
}