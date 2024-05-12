using Mentorly.Infrastructure.Security;
using System.Security.Claims;

namespace Mentorly.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetId(this ClaimsPrincipal claimsPrincipal)
        => Guid.Parse(claimsPrincipal.FindFirstValue(CustomClaimTypes.UnprotectedUserId)!);
}
