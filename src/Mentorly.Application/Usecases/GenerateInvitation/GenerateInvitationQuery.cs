using ErrorOr;
using MediatR;

namespace Mentorly.Application.Usecases.GenerateInvitation;

public record GenerateInvitationQuery(Guid UserId)
    : IRequest<ErrorOr<string>>;
