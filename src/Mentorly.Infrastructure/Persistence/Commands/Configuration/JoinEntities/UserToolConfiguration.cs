using Mentorly.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentorly.Infrastructure.Persistence.Commands.Configuration.JoinEntities;

public class UserToolConfiguration : IEntityTypeConfiguration<UserTool>
{
    public void Configure(EntityTypeBuilder<UserTool> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserTools)
            .HasForeignKey(x => x.UserCode);

        builder
            .HasOne(x => x.Tool)
            .WithMany(x => x.UserTools)
            .HasForeignKey(x => x.ToolCode);
    }
}
