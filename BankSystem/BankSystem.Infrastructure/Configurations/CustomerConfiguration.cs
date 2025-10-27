using BankSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Infrastructure.Configurations;

public class CustomerConfiguration : BaseEntityConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);

        builder.ToTable("Customers");

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.NationalId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.DateOfBirth)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(e => e.Type)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasIndex(e => e.Email)
            .IsUnique();

        builder.HasIndex(e => e.NationalId)
            .IsUnique();

        builder.HasIndex(e => e.PhoneNumber);
        builder.HasIndex(e => new { e.FirstName, e.LastName });

        builder.HasMany(c => c.Accounts)
            .WithOne(a => a.Customer)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Restrict); // prevents deletion of customer with existing accounts
    }
}