using ErrorOr;
using MediatR;

namespace Mentorly.Application.Usecases.ContinueWithGoogle;

public record ContinueWithGoogleCommand(
    string IDToken,
    string? InvitationCode = null)
    : IRequest<ErrorOr<string>>;
