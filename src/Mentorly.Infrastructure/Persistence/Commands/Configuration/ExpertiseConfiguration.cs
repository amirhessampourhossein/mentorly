using Mentorly.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentorly.Infrastructure.Persistence.Commands.Configuration;

public class ExpertiseConfiguration : IEntityTypeConfiguration<Expertise>
{
    public void Configure(EntityTypeBuilder<Expertise> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.Disciplines)
            .WithOne(x => x.Expertise)
            .HasForeignKey(x => x.ExpertiseCode);

        builder
            .HasMany(x => x.UserExpertises)
            .WithOne(x => x.Expertise)
            .HasForeignKey(x => x.ExpertiseCode);
    }
}
