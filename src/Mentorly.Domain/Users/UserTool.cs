using Mentorly.Domain.Common;

namespace Mentorly.Domain.Users;

public class UserTool : Entity
{
    public User User { get; set; } = null!;

    public Guid UserCode { get; set; }

    public Tool Tool { get; set; } = null!;

    public Guid ToolCode { get; set; }
}
