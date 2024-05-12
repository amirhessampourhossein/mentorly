using ErrorOr;
using MediatR;
using Mentorly.Domain.Users;
using System.Collections.Immutable;

namespace Mentorly.Application.Usecases.GetExpertise;

public class GetExpertiseQueryHandler(IUserService userService)
    : IRequestHandler<GetExpertiseQuery, ErrorOr<IReadOnlyList<ExpertiseDto>>>
{
    public async Task<ErrorOr<IReadOnlyList<ExpertiseDto>>> Handle(
        GetExpertiseQuery request,
        CancellationToken cancellationToken)
    {
        return (await userService.GetExpertiseAsync(cancellationToken))
            .Select(ExpertiseDto.FromEntity)
            .ToImmutableList();
    }
}
