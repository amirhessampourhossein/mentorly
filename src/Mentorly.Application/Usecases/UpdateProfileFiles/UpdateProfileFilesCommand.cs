using ErrorOr;
using MediatR;

namespace Mentorly.Application.Usecases.UpdateProfileFiles;

public record UpdateProfileFilesCommand(
    Guid UserId,
    byte[]? Photo,
    byte[]? Resume) : IRequest<ErrorOr<Updated>>;