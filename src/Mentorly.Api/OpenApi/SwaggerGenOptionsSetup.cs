using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Mentorly.Api.OpenApi;

public sealed class SwaggerGenOptionsSetup
    : IConfigureNamedOptions<SwaggerGenOptions>
{
    private const string SecurityDefinitionName = "Google Authentication";

    public void Configure(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new()
        {
            Version = "v1",
            Title = "Mentorly API",
            Contact = new()
            {
                Name = "Developer",
                Email = "amirhesamph81@gmail.com"
            }
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        options.IncludeXmlComments(xmlPath);

        options.AddSecurityDefinition(
            SecurityDefinitionName,
            new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Name = "Authorization",
                Description = "Please provide a valid JWT (no need to specify the Bearer phrase)",
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

        options.AddSecurityRequirement(new()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = SecurityDefinitionName
                    }
                },
                Array.Empty<string>()
            }
        });

        options.SchemaFilter<EnumSchemaFilter>();
    }

    public void Configure(string? name, SwaggerGenOptions options) => Configure(options);
}
