using ErrorOr;
using MediatR;

namespace Mentorly.Application.Usecases.RevokeInvitation;

public record RevokeInvitationCommand(Guid UserId)
    : IRequest<ErrorOr<Deleted>>;
