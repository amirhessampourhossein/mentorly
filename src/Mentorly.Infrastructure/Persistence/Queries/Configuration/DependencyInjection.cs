using Mentorly.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Mentorly.Persistence.Queries.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddDapper<TContext>(
        this IServiceCollection services,
        Action<DapperContextOptions> optionsAction)
        where TContext : class, IDapperContext
    {
        var options = new DapperContextOptions();

        optionsAction(options);

        services.AddScoped<IDapperContext, TContext>(sp =>
            Activator.CreateInstance(typeof(TContext), options) is not TContext context
            ? throw new InvalidOperationException($"Cannot create instance of type '{nameof(TContext)}'")
            : context);

        return services;
    }
}
