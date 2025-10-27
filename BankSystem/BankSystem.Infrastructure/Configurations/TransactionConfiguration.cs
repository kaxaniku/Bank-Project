using BankSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Infrastructure.Configurations;

public class TransactionConfiguration : BaseEntityConfiguration<Transaction>
{
    public override void Configure(EntityTypeBuilder<Transaction> builder)
    {
        base.Configure(builder);

        builder.ToTable("Transactions");

        builder.Property(e => e.SourceAccountId)
            .IsRequired(false);

        builder.Property(e => e.DestinationAccountId)
            .IsRequired(false);

        builder.Property(e => e.Type)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(e => e.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(e => e.Currency)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(e => e.FeeAmount)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0m);

        builder.Property(e => e.FeeCurrency)
            .HasMaxLength(3);

        builder.Property(e => e.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(e => e.Reference)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Description)
            .HasMaxLength(500);

        builder.Property(e => e.SourceBalanceAfter)
            .HasColumnType("decimal(18,2)");

        builder.Property(e => e.DestinationBalanceAfter)
            .HasColumnType("decimal(18,2)");

        builder.HasIndex(e => e.Reference)
            .IsUnique();

        builder.HasIndex(e => e.SourceAccountId);
        builder.HasIndex(e => e.DestinationAccountId);
        builder.HasIndex(e => e.Status);
        builder.HasIndex(e => e.Type);
        builder.HasIndex(e => e.CreatedAt);

        builder.HasIndex(e => new { e.SourceAccountId, e.CreatedAt });
        builder.HasIndex(e => new { e.DestinationAccountId, e.CreatedAt });
        builder.HasIndex(e => new { e.Status, e.CreatedAt });

        builder.HasOne(t => t.SourceAccount)
            .WithMany()
            .HasForeignKey(t => t.SourceAccountId)
            .OnDelete(DeleteBehavior.Restrict); // prevents deletion of accounts involved in transactions

        builder.HasOne(t => t.DestinationAccount)
            .WithMany()
            .HasForeignKey(t => t.DestinationAccountId)
            .OnDelete(DeleteBehavior.Restrict); // prevents deletion of accounts involved in transactions
    }
}