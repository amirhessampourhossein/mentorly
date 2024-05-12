using ErrorOr;
using MediatR;
using Mentorly.Domain.Users;
using System.Collections.Immutable;

namespace Mentorly.Application.Usecases.GetDisciplines;

public class GetDisciplineQueryHandler(IUserService userService)
    : IRequestHandler<GetDisciplinesQuery, ErrorOr<IReadOnlyList<DisciplineDto>>>
{
    public async Task<ErrorOr<IReadOnlyList<DisciplineDto>>> Handle(GetDisciplinesQuery request, CancellationToken cancellationToken)
    {
        return (await userService.GetDisciplinesAsync(cancellationToken))
            .Select(DisciplineDto.FromEntity)
            .ToImmutableList();
    }
}
