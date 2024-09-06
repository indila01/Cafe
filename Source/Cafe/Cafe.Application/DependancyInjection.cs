using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class DependancyInjection
{
    /// <summary>
    /// Registers the application services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>service collection</returns>
    public static IServiceCollection RegisterApplicationServices(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}