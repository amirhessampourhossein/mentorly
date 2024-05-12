using Mentorly.Domain.Users;

namespace Mentorly.Application.Usecases.GetSkills;

public record SkillDto(
    Guid Id,
    string Type,
    string Title)
{
    public static SkillDto FromEntity(Skill skill)
        => new(
            skill.Id,
            skill.Type.ToString(),
            skill.Title);
}
