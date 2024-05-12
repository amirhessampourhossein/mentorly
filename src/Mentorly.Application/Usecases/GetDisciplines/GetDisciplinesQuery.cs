using ErrorOr;
using MediatR;

namespace Mentorly.Application.Usecases.GetDisciplines;

public record GetDisciplinesQuery : IRequest<ErrorOr<IReadOnlyList<DisciplineDto>>>;
