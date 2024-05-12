using Mentorly.Application.Services;
using Mentorly.Domain.Users;
using Mentorly.Infrastructure.Security;
using Mentorly.Infrastructure.Services;
using Mentorly.Infrastructure.Services.MeetManager;
using Mentorly.Persistence.Commands;
using Mentorly.Persistence.Queries;
using Mentorly.Persistence.Queries.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mentorly.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        services
            .Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)))
            .Configure<GoogleOptions>(configuration.GetSection(nameof(GoogleOptions)));

        services
            .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString))
            .AddDapper<DapperContext>(options => options.UseSqlServer(connectionString));

        services.AddHttpClient();
        services.AddDataProtection();
        services.AddScoped<IUserService, UserService>();
        services.AddTransient<IClaimsTransformation, ClaimsTransformer>();
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IMeetManager, GoogleMeetManager>();

        var jwtOptions = new JwtOptions();
        configuration.Bind(nameof(JwtOptions), jwtOptions);

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidIssuer = jwtOptions.ValidIssuer,
                    ValidAudience = jwtOptions.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                    ValidateIssuerSigningKey = true,
                };
            });

        services.AddAuthorization();

        services.AddAntiforgery();

        return services;
    }
}
