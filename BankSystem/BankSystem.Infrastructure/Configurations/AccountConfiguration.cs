using BankSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Infrastructure.Configurations;

public class AccountConfiguration : BaseEntityConfiguration<Account>
{
    public override void Configure(EntityTypeBuilder<Account> builder)
    {
        base.Configure(builder);

        builder.ToTable("Accounts");

        builder.Property(e => e.AccountNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Currency)
            .IsRequired()
            .HasMaxLength(3)
            .HasDefaultValue("USD");

        builder.Property(e => e.Balance)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0m);

        builder.Property(e => e.Type)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(e => e.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(e => e.CustomerId)
            .IsRequired();

        builder.HasIndex(e => e.AccountNumber)
            .IsUnique();

        builder.HasIndex(e => e.CustomerId);
        builder.HasIndex(e => e.Status);
        builder.HasIndex(e => new { e.CustomerId, e.Type });

        builder.HasOne(a => a.Customer)
            .WithMany(c => c.Accounts)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Restrict); // prevents deletion of customer with existing accounts

        builder.HasMany(a => a.Cards)
            .WithOne(c => c.Account)
            .HasForeignKey(c => c.AccountId)
            .OnDelete(DeleteBehavior.Restrict); // prevents deletion of customer with existing accounts
    }
}