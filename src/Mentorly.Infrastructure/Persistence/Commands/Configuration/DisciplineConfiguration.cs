using Mentorly.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentorly.Infrastructure.Persistence.Commands.Configuration;

public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
{
    public void Configure(EntityTypeBuilder<Discipline> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Expertise)
            .WithMany(x => x.Disciplines)
            .HasForeignKey(x => x.ExpertiseCode);

        builder
            .HasMany(x => x.UserDisciplines)
            .WithOne(x => x.Discipline)
            .HasForeignKey(x => x.DisciplineCode);
    }
}
