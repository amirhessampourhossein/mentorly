using Mentorly.Domain.Common;

namespace Mentorly.Domain.Users;

public class Skill : Entity
{
    public string Title { get; set; } = null!;

    public SkillType Type { get; set; } = SkillType.Unknown;

    public virtual ICollection<UserSkill> UserSkills { get; set; } = null!;
}
