using Mentorly.Domain.Bookings;
using Mentorly.Domain.Common;
using Mentorly.Domain.Transactions;
using Mentorly.Domain.Users;

namespace Mentorly.Domain.Sessions;

public class Session : Entity
{
    public Booking Booking { get; set; } = null!;

    public Guid BookingCode { get; set; }

    public User Interviewer { get; set; } = null!;

    public Guid InterviewerCode { get; set; }

    public User Interviewee { get; set; } = null!;

    public Guid IntervieweeCode { get; set; }

    public SessionStatus Status { get; set; }

    public string Feedback { get; set; } = null!;

    public int InterviewerRating { get; set; }

    public int IntervieweeRating { get; set; }

    public bool PaymentStatus { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = null!;
}