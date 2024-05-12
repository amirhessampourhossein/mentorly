using Mentorly.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentorly.Infrastructure.Persistence.Commands.Configuration;

public class ToolConfiguration : IEntityTypeConfiguration<Tool>
{
    public void Configure(EntityTypeBuilder<Tool> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.UserTools)
            .WithOne(x => x.Tool)
            .HasForeignKey(x => x.ToolCode);
    }
}
