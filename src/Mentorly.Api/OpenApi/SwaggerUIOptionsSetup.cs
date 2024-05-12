using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Mentorly.Api.OpenApi;

public sealed class SwaggerUIOptionsSetup : IConfigureNamedOptions<SwaggerUIOptions>
{
    public void Configure(SwaggerUIOptions options)
    {
        options.DocumentTitle = "Mentorly";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Mentorly API v1");
    }

    public void Configure(string? name, SwaggerUIOptions options) => Configure(options);
}
