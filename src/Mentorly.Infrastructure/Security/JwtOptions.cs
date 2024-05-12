namespace Mentorly.Infrastructure.Security;

public class JwtOptions
{
    public string Secret { get; set; } = null!;

    public string ValidIssuer { get; set; } = null!;

    public string ValidAudience { get; set; } = null!;

    public int TokenLifetime { get; set; }
}
