using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.Consul.Extensions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Infrastructure.Consul;

[InjectAsSingleton(typeof(IConsulHttpApiClient))]
internal sealed class ConsulHttpApiClient : IConsulHttpApiClient
{
    private static TimeSpan HttpClientTimeout => TimeSpan.FromSeconds(30);
    
    private readonly IConsulEndpointConfig consulEndpointConfig;
    private readonly bool isHttpContentAutoDisposable;
    private HttpClient httpClient;

    private bool MustDisposeHttpContent => !isHttpContentAutoDisposable;

    public ConsulHttpApiClient(
        IConsulEndpointConfig consulEndpointConfig,
        IConsulHttpClientFactory httpClientFactory)
    {
        this.consulEndpointConfig = consulEndpointConfig;
        this.isHttpContentAutoDisposable = httpClientFactory.IsHttpContentAutoDisposable;
        httpClient = httpClientFactory.CreateHttpClient();
        httpClient.Timeout = HttpClientTimeout;
        httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };

        this.AgentApi = new ConsulAgentApiClientProxy(
            httpClient,
            consulEndpointConfig,
            MustDisposeHttpContent);
        this.CatalogApi = new ConsulCatalogApiClientProxy(
            httpClient,
            consulEndpointConfig,
            MustDisposeHttpContent);
    }

    public void Dispose()
    {
        httpClient?.Dispose();
        httpClient = null;
    }

    public Task<HttpResponseMessage> GetAsync(
        string keyPath,
        string uriQuery,
        CancellationToken cancellationToken)
    {
        var url = consulEndpointConfig.GetConfig().GetKeyUrl(keyPath, uriQuery);

        return httpClient.GetAsync(url, cancellationToken);
    }

    public async Task SaveKeyValueAsync(string keyPath, string value, CancellationToken cancellationToken)
    {
        var url = consulEndpointConfig.GetConfig().GetKeyUrl(keyPath);

        using var content = value.CreateHttpStringContentWrapper(MustDisposeHttpContent);

        using var response = await httpClient
            .PutAsync(url, content.HttpContent, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCodeEx();
    }

    public async Task DeleteKeysByPrefixAsync(string keyPrefix, CancellationToken cancellationToken)
    {
        var url = consulEndpointConfig.GetConfig().GetKeyUrl(keyPrefix, "recurse=true");

        using var response = await httpClient
            .DeleteAsync(url, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCodeEx();
    }

    public IConsulAgentApiClient AgentApi { get; }
    public IConsulCatalogApiClient CatalogApi { get; }

    public IConsulSessionalKeyValueApi CreateSessionalKeyValueApiClient(IConsulSessionNamingStrategy sessionNamingStrategy,
        IConsulSessionMonitoring consulSessionMonitoring)
    {
        return new ConsulSessionalKeyValueApiClientProxy(
            httpClient,
            consulEndpointConfig,
            MustDisposeHttpContent,
            sessionNamingStrategy,
            consulSessionMonitoring);
    }
}