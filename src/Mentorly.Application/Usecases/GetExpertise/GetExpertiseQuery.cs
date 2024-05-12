using ErrorOr;
using MediatR;

namespace Mentorly.Application.Usecases.GetExpertise;

public record GetExpertiseQuery : IRequest<ErrorOr<IReadOnlyList<ExpertiseDto>>>;
