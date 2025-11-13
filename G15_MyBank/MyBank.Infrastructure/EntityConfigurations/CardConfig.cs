using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Domain;

namespace MyBank.Infrastructure.EntityConfigurations;

internal class CardConfig : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder
            .HasIndex(c => c.CardNumber)
            .IsUnique();
    }
}
