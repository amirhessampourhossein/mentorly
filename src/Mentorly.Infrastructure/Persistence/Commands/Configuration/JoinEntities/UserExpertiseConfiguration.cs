using Mentorly.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentorly.Infrastructure.Persistence.Commands.Configuration.JoinEntities;

public class UserExpertiseConfiguration : IEntityTypeConfiguration<UserExpertise>
{
    public void Configure(EntityTypeBuilder<UserExpertise> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserExpertises)
            .HasForeignKey(x => x.UserCode);

        builder
            .HasOne(x => x.Expertise)
            .WithMany(x => x.UserExpertises)
            .HasForeignKey(x => x.ExpertiseCode);
    }
}
