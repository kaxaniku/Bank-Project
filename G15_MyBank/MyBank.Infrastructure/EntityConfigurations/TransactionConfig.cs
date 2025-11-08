using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Domain;

namespace MyBank.Infrastructure.EntityConfigurations;
internal class TransactionConfig : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder
            .HasOne(t => t.FromAccount)
            .WithMany(a => a.TransactionsSent)
            .HasForeignKey(t => t.FromAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(t => t.ToAccount)
            .WithMany(a => a.TransactionsReceived)
            .HasForeignKey(t => t.ToAccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
