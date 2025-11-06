using BankSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Infrastructure.Configurations;

public class CardConfiguration : BaseEntityConfiguration<Card>
{
    public override void Configure(EntityTypeBuilder<Card> builder)
    {
        base.Configure(builder);

        builder.ToTable("Cards");

        builder.Property(e => e.CardNumber)
            .IsRequired();

        builder.Property(e => e.CardHolderName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.ExpirationDate)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(e => e.CVV)
            .IsRequired()
            .HasMaxLength(4);

        builder.Property(e => e.Type)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Status)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.AccountId)
            .IsRequired();

        builder.HasIndex(e => e.CardNumber)
            .IsUnique();

        builder.HasIndex(e => e.AccountId);
        builder.HasIndex(e => e.Status);
        builder.HasIndex(e => e.ExpirationDate);

        builder.HasOne(c => c.Account)
            .WithMany(a => a.Cards)
            .HasForeignKey(c => c.AccountId)
            .OnDelete(DeleteBehavior.Restrict); // prevents deletion of account with existing cards
    }
}