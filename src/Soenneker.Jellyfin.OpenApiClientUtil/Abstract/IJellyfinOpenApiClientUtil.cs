using Soenneker.Jellyfin.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Jellyfin.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IJellyfinOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<JellyfinOpenApiClient> Get(CancellationToken cancellationToken = default);
}
