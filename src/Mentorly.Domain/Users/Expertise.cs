using Mentorly.Domain.Common;

namespace Mentorly.Domain.Users;

public class Expertise : Entity
{
    public string Title { get; set; } = null!;

    public virtual ICollection<Discipline> Disciplines { get; set; } = null!;

    public virtual ICollection<UserExpertise> UserExpertises { get; set; } = null!;
}