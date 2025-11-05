using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Domain.Interfaces;

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
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.FromAccount)
            .WithMany(a => a.TransactionsSent)
            .HasForeignKey(t => t.FromAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.ToAccount)
            .WithMany(a => a.TransactionsReceived)
            .HasForeignKey(t => t.ToAccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=.;Database=KNBank;Integrated Security = true;TrustServerCertificate=true");
    }
}
