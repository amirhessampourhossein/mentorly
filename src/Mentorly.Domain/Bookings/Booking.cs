using Mentorly.Domain.Common;
using Mentorly.Domain.Sessions;
using Mentorly.Domain.Users;

namespace Mentorly.Domain.Bookings;

public class Booking : Entity
{
    public virtual User User { get; set; } = null!;

    public Guid UserCode { get; set; }

    public DateTime AvailableDateTime { get; set; }

    public bool IsReserved { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = null!;
}