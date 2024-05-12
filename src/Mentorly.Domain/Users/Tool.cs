using Mentorly.Domain.Common;

namespace Mentorly.Domain.Users;

public class Tool : Entity
{
    public string Title { get; set; } = null!;

    public virtual ICollection<UserTool> UserTools { get; set; } = null!;
}
