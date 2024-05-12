using Mentorly.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentorly.Infrastructure.Persistence.Commands.Configuration.JoinEntities;

public class UserDisciplineConfiguration : IEntityTypeConfiguration<UserDiscipline>
{
    public void Configure(EntityTypeBuilder<UserDiscipline> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserDisciplines)
            .HasForeignKey(x => x.UserCode);

        builder
            .HasOne(x => x.Discipline)
            .WithMany(x => x.UserDisciplines)
            .HasForeignKey(x => x.DisciplineCode);
    }
}
