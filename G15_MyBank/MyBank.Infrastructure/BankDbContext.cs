using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Domain.Interfaces;

namespace MyBank.Infrastructure;
public class BankDbContext : DbContext
{
    public DbSet<Account>? Accounts { get; }
    public DbSet<Card>? Cards { get; }
    public DbSet<City>? Cities { get; }
    public DbSet<Country>? Countries { get; }
    public DbSet<Customer>? Customers { get; }
    public DbSet<Transaction>? Transactions { get; }

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

        modelBuilder.Entity<Account>()
            .HasIndex(a => a.AccountNumber)
            .IsUnique();

        modelBuilder.Entity<Card>()
            .HasIndex(c => c.CardNumber)
            .IsUnique();

        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.PersonalNumber)
            .IsUnique();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        //TODO: Move connection string to configuration file
        optionsBuilder.UseSqlServer("Server=.;Database=KNBank;Integrated Security = true;TrustServerCertificate=true");
    }
}
