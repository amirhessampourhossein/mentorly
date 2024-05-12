namespace Mentorly.Application.Usecases.BecomeInterviewer;

public record UpdateProfessionalProfileRequest(
    int? YearsOfExperience,
    int? MonthsOfExperience,
    string? LinkedIn,
    string? ProfessionalBio,
    string[]? Expertise,
    string[]? Disciplines,
    string[]? Skills,
    string[]? Tools)
{
    public UpdateProfessionalProfileCommand ToCommand(Guid userId)
        => new(
            userId,
            YearsOfExperience,
            MonthsOfExperience,
            LinkedIn,
            Expertise,
            Disciplines,
            Skills,
            Tools);
}
