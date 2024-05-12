using ErrorOr;
using MediatR;
using Mentorly.Domain.Users;

namespace Mentorly.Application.Usecases.UpdateProfile;

public record UpdateUserProfileCommand(
    Guid UserId,
    string? FirstName,
    string? LastName,
    Gender? Gender,
    string? Country,
    DateTime? BirthDate,
    string? Bio,
    string? Mobile,
    string? Language,
    string? Address) : IRequest<ErrorOr<Updated>>;
