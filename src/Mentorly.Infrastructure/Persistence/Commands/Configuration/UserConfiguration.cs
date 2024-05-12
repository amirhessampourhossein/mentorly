using Mentorly.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentorly.Persistence.Commands.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.AffiliateUser)
            .WithMany(x => x.AffiliatedUsers)
            .HasForeignKey(x => x.AffiliateCode);

        builder
            .HasMany(x => x.Bookings)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserCode);

        builder
            .HasMany(x => x.SessionsAsInterviewer)
            .WithOne(x => x.Interviewer)
            .HasForeignKey(x => x.InterviewerCode);

        builder
            .HasMany(x => x.SessionsAsInterviewee)
            .WithOne(x => x.Interviewee)
            .HasForeignKey(x => x.IntervieweeCode);

        builder
            .HasMany(x => x.UserExpertises)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserCode);

        builder
            .HasMany(x => x.UserDisciplines)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserCode);

        builder
            .HasMany(x => x.UserSkills)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserCode);

        builder
            .HasMany(x => x.UserTools)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserCode);
    }
}
