using ErrorOr;
using MediatR;
using Mentorly.Domain.Users;

namespace Mentorly.Application.Usecases.UpdateProfileFiles;

public class UpdateProfileFilesCommandHandler(IUserService userService)
    : IRequestHandler<UpdateProfileFilesCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(UpdateProfileFilesCommand request, CancellationToken cancellationToken)
    {
        var user = (await userService.GetByIdAsync(request.UserId, cancellationToken))!;

        user.ProfilePhoto = request.Photo ?? user.ProfilePhoto;
        user.Resume = request.Resume ?? user.Resume;

        await userService.UpdateAsync(user, cancellationToken);

        return Result.Updated;
    }
}
