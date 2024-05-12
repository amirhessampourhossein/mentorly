using ErrorOr;
using MediatR;
using Mentorly.Application.Services;
using Mentorly.Domain.Users;

namespace Mentorly.Application.Usecases.ContinueWithGoogle;

public class ContinueWithGoogleCommandHandler(
    IUserService userService,
    IAuthService authenticationService)
    : IRequestHandler<ContinueWithGoogleCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(
        ContinueWithGoogleCommand request,
        CancellationToken cancellationToken)
    {
        var user = await authenticationService.CreateUserFromIDTokenAsync(request.IDToken, cancellationToken);

        if (request.InvitationCode is not null)
        {
            var affiliateCode = await userService.GetIdFromInvitationAsync(
                request.InvitationCode,
                cancellationToken);

            user.AffiliateCode = affiliateCode;
        }

        var isUserEmailRegistered = await userService.IsEmailRegisteredAsync(
            user.Email,
            cancellationToken);

        if (!isUserEmailRegistered)
            user = await userService.AddAsync(user, cancellationToken);
        else
            user = await userService.GetByEmailAsync(user.Email, cancellationToken);

        var token = authenticationService.GenerateJwt(user!);

        return token;
    }
}
