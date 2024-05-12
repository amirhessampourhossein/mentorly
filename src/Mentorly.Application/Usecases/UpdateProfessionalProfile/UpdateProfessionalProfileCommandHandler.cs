using ErrorOr;
using MediatR;
using Mentorly.Domain.Users;

namespace Mentorly.Application.Usecases.BecomeInterviewer;

public class UpdateProfessionalProfileCommandHandler(IUserService userService)
    : IRequestHandler<UpdateProfessionalProfileCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(UpdateProfessionalProfileCommand request, CancellationToken cancellationToken)
    {
        var user = (await userService.GetByIdAsync(request.UserId, cancellationToken))!;

        user.IsInterviewer = true;
        user.LinkedIn = request.LinkedIn ?? user.LinkedIn;
        user.YearsOfExperience = request.YearsOfExperience ?? user.YearsOfExperience;
        user.MonthsOfExperience = request.MonthsOfExperience ?? user.MonthsOfExperience;

        await userService.UpdateAsync(user, cancellationToken);

        await userService.UpdateUserExpertiseAsync(
            user.Id,
            request.Expertise,
            cancellationToken);

        await userService.UpdateUserDisciplinesAsync(
            user.Id,
            request.Disciplines,
            cancellationToken);

        await userService.UpdateUserSkillsAsync(
            user.Id,
            request.Skills,
            cancellationToken);

        await userService.UpdateUserToolsAsync(
            user.Id,
            request.Tools,
            cancellationToken);

        return Result.Updated;
    }
}
