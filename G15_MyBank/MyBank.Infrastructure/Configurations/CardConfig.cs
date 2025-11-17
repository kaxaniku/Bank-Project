using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Domain;
using Microsoft.EntityFrameworkCore;

namespace MyBank.Infrastructure.Configurations
{
    internal class CardConfig : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
             builder
            .HasIndex(c => c.CardNumber)
            .IsUnique();
        }
    }
}
