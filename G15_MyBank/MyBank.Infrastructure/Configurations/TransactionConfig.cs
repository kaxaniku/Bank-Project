using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Domain;
using System.Reflection.Emit;

namespace MyBank.Infrastructure.Configurations
{
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
                .WithMany(a => a.TransactionsRecived)
                .HasForeignKey(t => t.ToAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .ToTable(t => t.HasCheckConstraint("CK_Transactions_Amount_Positive", "[Amount] >= 0"));
        }
    }
}
