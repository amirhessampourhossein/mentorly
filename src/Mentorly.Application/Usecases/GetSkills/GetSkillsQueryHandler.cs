using ErrorOr;
using MediatR;
using Mentorly.Domain.Users;
using System.Collections.Immutable;

namespace Mentorly.Application.Usecases.GetSkills;

public class GetSkillsQueryHandler(IUserService userService)
    : IRequestHandler<GetSkillsQuery, ErrorOr<IReadOnlyList<SkillDto>>>
{
    public async Task<ErrorOr<IReadOnlyList<SkillDto>>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
    {
        return (await userService.GetSkillsAsync(cancellationToken))
            .Select(SkillDto.FromEntity)
            .ToImmutableList();
    }
}
