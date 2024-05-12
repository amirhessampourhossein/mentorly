using Mentorly.Domain.Users;

namespace Mentorly.Application.Usecases.GetDisciplines;

public record DisciplineDto(
    Guid Id,
    Guid ExpertiseCode,
    string Title)
{
    public static DisciplineDto FromEntity(Discipline discipline)
        => new(
            discipline.Id,
            discipline.Expertise.Id,
            discipline.Title);
}
