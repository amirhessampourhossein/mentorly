using ErrorOr;
using MediatR;
using Mentorly.Domain.Users;
using System.Collections.Immutable;

namespace Mentorly.Application.Usecases.GetTools;

public class GetToolsQueryHandler(IUserService userService)
    : IRequestHandler<GetToolsQuery, ErrorOr<IReadOnlyList<ToolDto>>>
{
    public async Task<ErrorOr<IReadOnlyList<ToolDto>>> Handle(GetToolsQuery request, CancellationToken cancellationToken)
    {
        return (await userService.GetToolsAsync(cancellationToken))
            .Select(ToolDto.FromEntity)
            .ToImmutableList();
    }
}
