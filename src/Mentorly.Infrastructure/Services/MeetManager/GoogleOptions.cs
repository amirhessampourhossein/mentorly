namespace Mentorly.Infrastructure.Services.MeetManager;

public class GoogleOptions
{
    public string ClientId { get; set; } = null!;

    public string ClientSecret { get; set; } = null!;

    public string[] Scopes { get; set; } = null!;
}
