using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Claims;

namespace Mentorly.Infrastructure.Security;

public class ClaimsTransformer(IDataProtectionProvider dataProtectionProvider)
    : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var protector = dataProtectionProvider.CreateProtector(SecurityConstants.DataProtectionPurpose);

        var protectedId = principal.FindFirstValue(CustomClaimTypes.ProtectedUserId)!;

        var userId = protector.Unprotect(protectedId);

        var newIdentity = new ClaimsIdentity(SecurityConstants.AuthenticationType);

        newIdentity.AddClaim(new(CustomClaimTypes.UnprotectedUserId, userId));

        principal.AddIdentity(newIdentity);

        return Task.FromResult(principal);
    }
}
