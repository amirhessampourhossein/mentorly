using ErrorOr;
using MediatR;
using Mentorly.Domain.Users;

namespace Mentorly.Application.Usecases.GenerateInvitation;

public class GenerateInvitationQueryHandler(IUserService userRepository)
    : IRequestHandler<GenerateInvitationQuery, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(
        GenerateInvitationQuery request,
        CancellationToken cancellationToken)
    {
        var token = await userRepository.GetOrCreateInvitationAsync(
            request.UserId,
            cancellationToken);

        return token;
    }
}
