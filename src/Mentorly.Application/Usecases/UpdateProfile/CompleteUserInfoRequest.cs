using Mentorly.Domain.Users;

namespace Mentorly.Application.Usecases.UpdateProfile;

public record UpdateUserProfileRequest(
    string? FirstName,
    string? LastName,
    string? Gender,
    string? Country,
    DateTime? BirthDate,
    string? Bio,
    string? Mobile,
    string? Language,
    string? Address)
{
    public UpdateUserProfileCommand ToCommand(Guid userId)
        => new(
            userId,
            FirstName,
            LastName,
            Gender is not null ? Enum.Parse<Gender>(Gender) : null,
            Country,
            BirthDate,
            Bio,
            Mobile,
            Language,
            Address);
}
