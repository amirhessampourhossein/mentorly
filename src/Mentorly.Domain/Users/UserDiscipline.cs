using Mentorly.Domain.Common;

namespace Mentorly.Domain.Users;

public class UserDiscipline : Entity
{
    public User User { get; set; } = null!;

    public Guid UserCode { get; set; }

    public Discipline Discipline { get; set; } = null!;

    public Guid DisciplineCode { get; set; }
}
