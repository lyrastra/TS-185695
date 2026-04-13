using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.Consul.Extensions;
using Moedelo.Infrastructure.Consul.Models;

namespace Moedelo.Infrastructure.Consul;

internal sealed class ConsulCatalogApiClientProxy : IConsulCatalogApiClient
{
    private readonly HttpClient httpClientRef;
    private readonly IConsulEndpointConfig consulEndpoint;
    /// <summary>
    /// Флаг, указывающий нужно ли автоматически освобождать ресурсы HttpContent после использования.
    /// В данном классе не используется, так как реализуются только GET-запросы без отправки контента.
    /// Поле добавлено для консистентности с ConsulAgentApiClientProxy.
    /// </summary>
    private readonly bool mustDisposeRequestHttpContent;

    internal ConsulCatalogApiClientProxy(
        HttpClient httpClientRef,
        IConsulEndpointConfig consulEndpoint,
        bool mustDisposeRequestHttpContent)
    {
        this.httpClientRef = httpClientRef;
        this.consulEndpoint = consulEndpoint;
        this.mustDisposeRequestHttpContent = mustDisposeRequestHttpContent;
    }

    public async Task<IReadOnlyCollection<string>> GetServiceIdsListAsync(CancellationToken cancellationToken)
    {
        var serviceNamesList = await GetAliveServiceNamesListAsync(cancellationToken).ConfigureAwait(false);

        var listOfServiceIdsList = await serviceNamesList
            .SelectParallelAsync(GetAliveServiceIdsListByNameAsync, maxDegreeOfParallelism: 10, cancellationToken)
            .ConfigureAwait(false);

        return new HashSet<string>(listOfServiceIdsList.SelectMany(serviceIds => serviceIds));
    }

    private async Task<IReadOnlyCollection<string>> GetAliveServiceNamesListAsync(CancellationToken cancellation)
    {
        var servicesUrl = consulEndpoint.GetConfig().GetCatalogApiMethodUrl("services");

        using var responseMessage = await httpClientRef
            .GetAsync(servicesUrl, cancellationToken: cancellation)
            .ConfigureAwait(false);

        responseMessage.EnsureSuccessStatusCodeEx();

        var servicesMap = await responseMessage
            .DeserializeJsonContentAsync<Dictionary<string, object>>(cancellation)
            .ConfigureAwait(false);

        return servicesMap.Keys.ToArray();
    }
    
    private async Task<IReadOnlyCollection<string>> GetAliveServiceIdsListByNameAsync(string serviceName, CancellationToken cancellation)
    {
        var url = consulEndpoint.GetConfig().GetCatalogApiMethodUrl($"service/{serviceName}");

        using var response = await httpClientRef
            .GetAsync(url, cancellationToken: cancellation)
            .ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            // м.б. сервис уже "исчез"
            return Array.Empty<string>();
        }

        var serviceInfos = await response
            .DeserializeJsonContentAsync<CatalogServiceInfoResponseBody[]>(cancellation)
            .ConfigureAwait(false); 

        return serviceInfos?.Select(info => info.ServiceID).ToArray() ?? Array.Empty<string>();
    }
}
