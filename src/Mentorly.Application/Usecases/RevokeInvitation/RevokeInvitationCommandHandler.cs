using ErrorOr;
using MediatR;
using Mentorly.Domain.Users;

namespace Mentorly.Application.Usecases.RevokeInvitation;

public class RevokeInvitationCommandHandler(IUserService userRepository)
    : IRequestHandler<RevokeInvitationCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(
        RevokeInvitationCommand request,
        CancellationToken cancellationToken)
    {
        await userRepository.RevokeInvitationAsync(
            request.UserId,
            cancellationToken);

        return Result.Deleted;
    }
}
