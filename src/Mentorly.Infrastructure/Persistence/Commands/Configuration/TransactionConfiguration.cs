using Mentorly.Domain.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentorly.Persistence.Commands.Configuration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Session)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => x.SessionCode);
    }
}
