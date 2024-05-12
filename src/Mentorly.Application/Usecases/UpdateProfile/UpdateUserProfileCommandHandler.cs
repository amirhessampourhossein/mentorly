using ErrorOr;
using MediatR;
using Mentorly.Domain.Users;

namespace Mentorly.Application.Usecases.UpdateProfile;

public class UpdateUserProfileCommandHandler(IUserService userRepository)
    : IRequestHandler<UpdateUserProfileCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(
        UpdateUserProfileCommand request,
        CancellationToken cancellationToken)
    {
        var user = (await userRepository.GetByIdAsync(request.UserId, cancellationToken))!;

        request
            .GetType()
            .GetProperties()
            .Where(property => property.Name != nameof(UpdateUserProfileCommand.UserId))
            .ToList()
            .ForEach(property =>
            {
                if (property.GetValue(request) is object value)
                    user
                    .GetType()
                    .GetProperty(property.Name)!
                    .SetValue(user, value);
            });

        await userRepository.UpdateAsync(user, cancellationToken);

        return Result.Updated;
    }
}
