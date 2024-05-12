using ErrorOr;
using MediatR;

namespace Mentorly.Application.Usecases.GetSkills;

public record GetSkillsQuery : IRequest<ErrorOr<IReadOnlyList<SkillDto>>>;
