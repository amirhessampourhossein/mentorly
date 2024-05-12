using Mentorly.Domain.Common;

namespace Mentorly.Domain.Users;

public class UserSkill : Entity
{
    public User User { get; set; } = null!;

    public Guid UserCode { get; set; }

    public Skill Skill { get; set; } = null!;

    public Guid SkillCode { get; set; }
}
