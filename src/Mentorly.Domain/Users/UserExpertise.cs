using Mentorly.Domain.Common;

namespace Mentorly.Domain.Users;

public class UserExpertise : Entity
{
    public User User { get; set; } = null!;

    public Guid UserCode { get; set; }

    public Expertise Expertise { get; set; } = null!;

    public Guid ExpertiseCode { get; set; }
}
