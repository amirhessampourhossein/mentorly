using ErrorOr;
using MediatR;

namespace Mentorly.Application.Usecases.GetTools;

public record GetToolsQuery : IRequest<ErrorOr<IReadOnlyList<ToolDto>>>;
