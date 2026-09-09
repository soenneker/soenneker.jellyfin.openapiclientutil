using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Jellyfin.HttpClients.Abstract;
using Soenneker.Jellyfin.OpenApiClientUtil.Abstract;
using Soenneker.Jellyfin.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Jellyfin.OpenApiClientUtil;

///<inheritdoc cref="IJellyfinOpenApiClientUtil"/>
public sealed class JellyfinOpenApiClientUtil : IJellyfinOpenApiClientUtil
{
    private readonly AsyncSingleton<JellyfinOpenApiClient> _client;

    public JellyfinOpenApiClientUtil(IJellyfinOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<JellyfinOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Jellyfin:ApiKey");
            string authHeaderName = configuration["Jellyfin:AuthHeaderName"] ?? "Authorization";
            string authHeaderValueTemplate = configuration["Jellyfin:AuthHeaderValueTemplate"] ?? "MediaBrowser Token={token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerName: authHeaderName, headerValue: authHeaderValue),
                httpClient: httpClient) { BaseUrl = httpClient.BaseAddress!.AbsoluteUri };

            return new JellyfinOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<JellyfinOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}

