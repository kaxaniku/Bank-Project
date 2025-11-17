using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyBank.Domain;
using MyBank.Domain.Interfaces;

namespace MyBank.Infrastructure
{
    public class MyBankDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

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
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyBankDbContext).Assembly,
            type => type.Namespace == "MyBank.Infrastructure.Configurations"
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {

                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = config.GetConnectionString("DefaultConnection");
                builder.UseSqlServer(connectionString);
            }
        }
    }
}
