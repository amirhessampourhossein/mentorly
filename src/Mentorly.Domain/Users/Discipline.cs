using Mentorly.Domain.Common;

namespace Mentorly.Domain.Users;

public class Discipline : Entity
{
    public Expertise Expertise { get; set; } = null!;

    public Guid ExpertiseCode { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<UserDiscipline> UserDisciplines { get; set; } = null!;
}
