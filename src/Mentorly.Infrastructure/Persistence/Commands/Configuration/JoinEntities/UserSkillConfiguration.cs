using Mentorly.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentorly.Infrastructure.Persistence.Commands.Configuration.JoinEntities;

public class UserSkillConfiguration : IEntityTypeConfiguration<UserSkill>
{
    public void Configure(EntityTypeBuilder<UserSkill> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserSkills)
            .HasForeignKey(x => x.UserCode);

        builder
            .HasOne(x => x.Skill)
            .WithMany(x => x.UserSkills)
            .HasForeignKey(x => x.SkillCode);
    }
}
