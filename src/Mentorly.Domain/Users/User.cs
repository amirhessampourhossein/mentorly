using Mentorly.Domain.Bookings;
using Mentorly.Domain.Common;
using Mentorly.Domain.Sessions;

namespace Mentorly.Domain.Users;

public class User : Entity
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public Gender? Gender { get; set; }

    public DateTime? BirthDate { get; set; }

    public byte[]? ProfilePhoto { get; set; }

    public string? Country { get; set; }

    public string? Mobile { get; set; }

    public User? AffiliateUser { get; set; }

    public Guid? AffiliateCode { get; set; }

    public string? NationalCode { get; set; }

    public string? AccountNumber { get; set; }

    public string? Bio { get; set; }

    public int? PricePerHour { get; set; }

    public string? Language { get; set; }

    public string? Address { get; set; }

    public string? InvitationCode { get; set; }

    public int? YearsOfExperience { get; set; }

    public int? MonthsOfExperience { get; set; }

    public byte[]? Resume { get; set; }

    public string? LinkedIn { get; set; }

    public bool? IsInterviewer { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsDeleted { get; set; }

    public virtual ICollection<User> AffiliatedUsers { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = null!;

    public virtual ICollection<Session> SessionsAsInterviewer { get; set; } = null!;

    public virtual ICollection<Session> SessionsAsInterviewee { get; set; } = null!;

    public virtual ICollection<UserExpertise> UserExpertises { get; set; } = null!;

    public virtual ICollection<UserDiscipline> UserDisciplines { get; set; } = null!;

    public virtual ICollection<UserSkill> UserSkills { get; set; } = null!;

    public virtual ICollection<UserTool> UserTools { get; set; } = null!;
}