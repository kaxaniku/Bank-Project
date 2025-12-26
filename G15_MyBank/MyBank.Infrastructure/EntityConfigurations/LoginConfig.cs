using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Domain;

namespace MyBank.Infrastructure.EntityConfigurations;

internal class LoginConfig : IEntityTypeConfiguration<Login>
{
    public void Configure(EntityTypeBuilder<Login> builder)
    {
        builder
            .HasIndex(l => l.Username)
            .IsUnique();
    }
}
