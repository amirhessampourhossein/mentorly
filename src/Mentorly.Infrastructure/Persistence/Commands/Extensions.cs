using Mentorly.Domain.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Mentorly.Persistence.Commands;

public static class ApplicationBuilderExtensions
{
    [Obsolete("Migrate to 'RecreateDatabase'")]
    public static IApplicationBuilder RecreateDatabaseForHost(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var dropScript = File.ReadAllText($"{AppContext.BaseDirectory}Persistence\\Scripts\\DropAll.sql");
        dbContext.Database.ExecuteSqlRaw(dropScript);
        dbContext.Database.EnsureCreated();

        var jsonPath = $"{AppContext.BaseDirectory}Persistence\\Externals";

        dbContext.ImportExpertiseJson($"{jsonPath}\\expertise.json");
        dbContext.ImportDisciplinesJson($"{jsonPath}\\disciplines.json");
        dbContext.ImportSkillsJson($"{jsonPath}\\skills.json");
        dbContext.ImportToolsJson($"{jsonPath}\\tools.json");

        return app;
    }

    public static IApplicationBuilder RecreateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        var jsonPath = $"{AppContext.BaseDirectory}Persistence\\Externals";

        dbContext.ImportExpertiseJson($"{jsonPath}\\expertise.json");
        dbContext.ImportDisciplinesJson($"{jsonPath}\\disciplines.json");
        dbContext.ImportSkillsJson($"{jsonPath}\\skills.json");
        dbContext.ImportToolsJson($"{jsonPath}\\tools.json");

        return app;
    }
}

public static class SetupExtensions
{
    public static void ImportExpertiseJson(
        this ApplicationDbContext dbContext,
        string filePath)
    {
        var expertiseDefinition = new[]
        {
            new
            {
                id = default(int),
                description = default(string)!
            }
        }.ToList();

        var json = File.ReadAllText(filePath);

        var expertise = JsonConvert
            .DeserializeAnonymousType(json, expertiseDefinition)!
            .Select(x => new Expertise()
            {
                Title = x.description
            })
            .ToArray();

        dbContext.Expertise.AddRange(expertise);

        dbContext.SaveChanges();
    }

    public static void ImportDisciplinesJson(
        this ApplicationDbContext dbContext,
        string filePath)
    {
        var disciplineDefinition = new[]
        {
            new
            {
                id = default(int),
                name = default(string)!,
                expertise = default(string)!
            }
        }.ToList();

        var json = File.ReadAllText(filePath);

        var disciplines = JsonConvert
            .DeserializeAnonymousType(json, disciplineDefinition)!
            .Select(x => new Discipline()
            {
                Title = x.name,
                ExpertiseCode = dbContext.Expertise.Single(e => e.Title == x.expertise).Id
            });

        dbContext.Disciplines.AddRange(disciplines);

        dbContext.SaveChanges();
    }

    public static void ImportSkillsJson(
        this ApplicationDbContext dbContext,
        string filePath)
    {
        var skillDefinition = new[]
        {
            new
            {
                id = default(int),
                name = default(string)!,
                skill_type = default(string)!
            }
        }.ToList();

        var json = File.ReadAllText(filePath);

        var skills = JsonConvert
            .DeserializeAnonymousType(json, skillDefinition)!
            .Select(x => new Skill()
            {
                Title = x.name,
                Type = Enum.Parse<SkillType>(x.skill_type)
            })
            .ToList();

        dbContext.Skills.AddRange(skills);

        dbContext.SaveChanges();
    }

    public static void ImportToolsJson(
        this ApplicationDbContext dbContext,
        string filePath)
    {
        var toolDefinition = new[]
        {
            new
            {
                id = default(int),
                name = default(string)!
            }
        }.ToList();

        var json = File.ReadAllText(filePath);

        var tools = JsonConvert
            .DeserializeAnonymousType(json, toolDefinition)!
            .Select(x => new Tool()
            {
                Title = x.name
            })
            .ToList();

        dbContext.Tools.AddRange(tools);

        dbContext.SaveChanges();
    }
}
