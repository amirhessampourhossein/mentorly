using Mentorly.Domain.Sessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentorly.Persistence.Commands.Configuration;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Booking)
            .WithMany(x => x.Sessions)
            .HasForeignKey(x => x.BookingCode);

        builder
            .HasOne(x => x.Interviewer)
            .WithMany(x => x.SessionsAsInterviewer)
            .HasForeignKey(x => x.InterviewerCode)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.Interviewee)
            .WithMany(x => x.SessionsAsInterviewee)
            .HasForeignKey(x => x.IntervieweeCode)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(x => x.Transactions)
            .WithOne(x => x.Session)
            .HasForeignKey(x => x.SessionCode);
    }
}
