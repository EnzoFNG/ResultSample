using Microsoft.Extensions.DependencyInjection;

namespace ResultSample.Abstractions.Mediator;

internal static class MediatorConfiguration
{
    internal static IServiceCollection AddMediatorConfiguration(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies
            (AppDomain.CurrentDomain.GetAssemblies()));

        return services;
    }
}