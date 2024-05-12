using Mentorly.Domain.Users;

namespace Mentorly.Application.Usecases.GetTools;

public record ToolDto(Guid Id, string Title)
{
    public static ToolDto FromEntity(Tool tool)
        => new(tool.Id, tool.Title);
}
