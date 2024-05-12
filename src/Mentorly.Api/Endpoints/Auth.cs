using MediatR;
using Mentorly.Api.Extensions;
using Mentorly.Application.Usecases.ContinueWithGoogle;
using Microsoft.AspNetCore.Mvc;

namespace Mentorly.Api.Endpoints;

public static class Auth
{
    public static RouteGroupBuilder MapAuthEndpoints(this RouteGroupBuilder group)
    {
        var authGroup = group.MapGroup("/auth");

        authGroup.MapGet("/continue-google", ContinueWithGoogleAsync);

        return group;
    }

    private static async Task<IResult> ContinueWithGoogleAsync(
    [FromHeader] string IDToken,
    [FromQuery] string? InvitationCode,
    ISender sender)
    {
        var command = new ContinueWithGoogleCommand(IDToken, InvitationCode);

        var response = await sender.Send(command);

        return response.ToResult();
    }
}
