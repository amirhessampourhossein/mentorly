using MediatR;
using Mentorly.Api.Extensions;
using Mentorly.Application.Usecases.BecomeInterviewer;
using Mentorly.Application.Usecases.GenerateInvitation;
using Mentorly.Application.Usecases.RevokeInvitation;
using Mentorly.Application.Usecases.UpdateProfile;
using Mentorly.Application.Usecases.UpdateProfileFiles;
using Microsoft.AspNetCore.Mvc;

namespace Mentorly.Api.Endpoints;

public static partial class Users
{
    public static RouteGroupBuilder MapUsersEndpoints(this RouteGroupBuilder group)
    {
        var usersGroup = group.MapGroup("/users");

        usersGroup
            .MapGet("/invitation", GenerateInvitationAsync)
            .RequireAuthorization();

        usersGroup
            .MapDelete("/invitation", RevokeInvitationAsync)
            .RequireAuthorization();

        usersGroup
            .MapPatch("/profile/general", UpdateProfileAsync)
            .RequireAuthorization();

        usersGroup
            .MapPatch("/profile/professional", UpdateProfessionalProfileAsync)
            .RequireAuthorization();

        usersGroup
            .MapPatch("/profile/files", UpdateProfileFilesAsync)
            .RequireAuthorization();

        return group;
    }

    private static async Task<IResult> UpdateProfileAsync(
        HttpContext httpContext,
        [FromBody] UpdateUserProfileRequest request,
        ISender sender)
    {
        var userId = httpContext.User.GetId();

        var command = request.ToCommand(userId);

        var response = await sender.Send(command);

        return response.ToResult();
    }

    private static async Task<IResult> GenerateInvitationAsync(
        HttpContext httpContext,
        ISender sender)
    {
        var userId = httpContext.User.GetId();

        var query = new GenerateInvitationQuery(userId);

        var response = await sender.Send(query);

        return response.ToResult();
    }

    private static async Task<IResult> RevokeInvitationAsync(
        HttpContext httpContext,
        ISender sender)
    {
        var userId = httpContext.User.GetId();

        var command = new RevokeInvitationCommand(userId);

        var response = await sender.Send(command);

        return response.ToResult();
    }

    private static async Task<IResult> UpdateProfessionalProfileAsync(
        HttpContext httpContext,
        ISender sender,
        [FromBody] UpdateProfessionalProfileRequest request)
    {
        var userId = httpContext.User.GetId();

        var command = request.ToCommand(userId);

        var response = await sender.Send(command);

        return response.ToResult();
    }

    private static async Task<IResult> UpdateProfileFilesAsync(
        HttpContext httpContext,
        ISender sender,
        [FromForm] IFormFile? resume,
        [FromForm] IFormFile? photo)
    {
        var userId = httpContext.User.GetId();

        var command = new UpdateProfileFilesCommand(
            userId,
            await photo.ToBytesAsync(),
            await resume.ToBytesAsync());

        var response = await sender.Send(command);

        return response.ToResult();
    }
}
