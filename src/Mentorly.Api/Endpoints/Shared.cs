using MediatR;
using Mentorly.Api.Extensions;
using Mentorly.Application.Usecases.GetDisciplines;
using Mentorly.Application.Usecases.GetExpertise;
using Mentorly.Application.Usecases.GetSkills;
using Mentorly.Application.Usecases.GetTools;

namespace Mentorly.Api.Endpoints;

public static class Shared
{
    public static RouteGroupBuilder MapSharedEndpoints(this RouteGroupBuilder app)
    {
        var group = app.MapGroup("/shared");

        group.MapGet("/expertise", GetExpertiseAsync);
        group.MapGet("/disciplines", GetDisciplinesAsync);
        group.MapGet("/skills", GetSkillsAsync);
        group.MapGet("/tools", GetToolsAsync);

        return app;
    }

    private static async Task<IResult> GetExpertiseAsync(ISender sender)
    {
        var response = await sender.Send(new GetExpertiseQuery());

        return response.ToResult();
    }

    private static async Task<IResult> GetDisciplinesAsync(ISender sender)
    {
        var response = await sender.Send(new GetDisciplinesQuery());

        return response.ToResult();
    }

    private static async Task<IResult> GetSkillsAsync(ISender sender)
    {
        var response = await sender.Send(new GetSkillsQuery());

        return response.ToResult();
    }

    private static async Task<IResult> GetToolsAsync(ISender sender)
    {
        var response = await sender.Send(new GetToolsQuery());

        return response.ToResult();
    }
}
