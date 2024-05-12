using Mentorly.Domain.Users;

namespace Mentorly.Application.Services;

public interface IAuthService
{
    Task<User> CreateUserFromIDTokenAsync(
        string IDToken,
        CancellationToken cancellationToken = default);

    string GenerateJwt(User user);
}
