using Mentorly.Domain.Users;

namespace Mentorly.Application.Usecases.GetExpertise;

public record ExpertiseDto(Guid Id, string Title)
{
    public static ExpertiseDto FromEntity(Expertise entity)
        => new(entity.Id, entity.Title);
}
