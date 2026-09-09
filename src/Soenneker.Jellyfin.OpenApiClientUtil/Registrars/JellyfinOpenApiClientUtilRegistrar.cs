using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Jellyfin.HttpClients.Registrars;
using Soenneker.Jellyfin.OpenApiClientUtil.Abstract;

namespace Soenneker.Jellyfin.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class JellyfinOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="JellyfinOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddJellyfinOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddJellyfinOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IJellyfinOpenApiClientUtil, JellyfinOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="JellyfinOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddJellyfinOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddJellyfinOpenApiHttpClientAsSingleton()
                .TryAddScoped<IJellyfinOpenApiClientUtil, JellyfinOpenApiClientUtil>();

        return services;
    }
}
