using ErrorOr;
using MediatR;

namespace Mentorly.Application.Usecases.BecomeInterviewer;

public record UpdateProfessionalProfileCommand(
    Guid UserId,
    int? YearsOfExperience,
    int? MonthsOfExperience,
    string? LinkedIn,
    string[]? Expertise,
    string[]? Disciplines,
    string[]? Skills,
    string[]? Tools) : IRequest<ErrorOr<Updated>>;
