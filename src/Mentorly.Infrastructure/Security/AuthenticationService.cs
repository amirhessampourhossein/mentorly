using Mentorly.Application.Services;
using Mentorly.Domain.Users;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mentorly.Infrastructure.Security;

public class AuthService(
    IDataProtectionProvider dataProtectionProvider,
    IHttpClientFactory httpClientFactory,
    IOptions<JwtOptions> options)
    : IAuthService
{
    public string GenerateJwt(User user)
    {
        var protector = dataProtectionProvider.CreateProtector(SecurityConstants.DataProtectionPurpose);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new(CustomClaimTypes.ProtectedUserId, protector.Protect(user.Id.ToString())),
            new(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new(JwtRegisteredClaimNames.Email, user.Email)
        };

        if (user.Mobile is not null)
            claims.Add(new(CustomClaimTypes.Mobile, user.Mobile));

        var securityToken = new JwtSecurityToken(
            options.Value.ValidIssuer,
            options.Value.ValidAudience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(options.Value.TokenLifetime),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public async Task<User> CreateUserFromIDTokenAsync(
        string IDToken,
        CancellationToken cancellationToken = default)
    {
        var securityToken = new JwtSecurityTokenHandler().ReadJwtToken(IDToken);

        var client = httpClientFactory.CreateClient();

        var pictureBytes = await client.GetByteArrayAsync(
            securityToken.Payload["picture"].ToString(),
            cancellationToken);

        return new()
        {
            FirstName = securityToken.Payload[JwtRegisteredClaimNames.GivenName].ToString()!,
            LastName = securityToken.Payload[JwtRegisteredClaimNames.FamilyName].ToString()!,
            Email = securityToken.Payload[JwtRegisteredClaimNames.Email].ToString()!,
            ProfilePhoto = pictureBytes
        };
    }
}
