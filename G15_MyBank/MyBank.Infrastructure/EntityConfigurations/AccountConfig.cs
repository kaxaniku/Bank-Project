using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Domain;

namespace MyBank.Infrastructure.EntityConfigurations;
internal class AccountConfig : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder
            .HasIndex(a => a.AccountNumber)
            .IsUnique();
    }
}
