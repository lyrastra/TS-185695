using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.Consul.Abstraction.Models;
using Moedelo.Infrastructure.Consul.Extensions;
using Moedelo.Infrastructure.Consul.Models;

namespace Moedelo.Infrastructure.Consul;

internal sealed class ConsulAgentApiClientProxy : IConsulAgentApiClient
{
    private readonly HttpClient httpClientRef;
    private readonly IConsulEndpointConfig consulEndpoint;
    /// <summary>
    /// Флаг, указывающий нужно ли автоматически освобождать ресурсы HttpContent после использования.
    /// Используется в HttpContentWrapper для контроля жизненного цикла объектов HttpContent.
    /// </summary>
    private readonly bool mustDisposeRequestHttpContent;

    internal ConsulAgentApiClientProxy(
        HttpClient httpClientRef,
        IConsulEndpointConfig consulEndpoint,
        bool mustDisposeRequestHttpContent)
    {
        this.httpClientRef = httpClientRef;
        this.consulEndpoint = consulEndpoint;
        this.mustDisposeRequestHttpContent = mustDisposeRequestHttpContent;
    }

    public async Task RegisterServiceAsync(
        AgentServiceRegistration registration,
        CancellationToken cancellationToken)
    {
        registration.ID.EnsureIsServiceIdValid();

        var url = consulEndpoint.GetConfig().GetAgentApiMethodUrl(
            "service/register", uriQuery: "replace-existing-checks=true");

        using var content = registration.CreateHttpJsonStringContentWrapper(mustDisposeRequestHttpContent); 

        using var responseMessage = await httpClientRef
            .PutAsync(url, content.HttpContent, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        responseMessage.EnsureSuccessStatusCodeEx();
    }

    public async Task DeregisterServiceAsync(
        string serviceId,
        CancellationToken cancellationToken)
    {
        serviceId.EnsureIsServiceIdValid();

        var url = consulEndpoint.GetConfig().GetAgentApiMethodUrl($"service/deregister/{serviceId}");
        
        using var responseMessage = await httpClientRef
            .PutAsync(url, content: null, cancellationToken)
            .ConfigureAwait(false);

        if (responseMessage.StatusCode == HttpStatusCode.NotFound)
        {
            return;
        }

        responseMessage.EnsureSuccessStatusCodeEx();
    }

    public async Task<bool> IsServiceRegisteredAsync(string serviceId, CancellationToken cancellationToken)
    {
        serviceId.EnsureIsServiceIdValid();

        var url = consulEndpoint.GetConfig()
            .GetAgentApiMethodUrl("services", @$"filter=ID == ""{serviceId}""");
        
        using var responseMessage = await httpClientRef
            .GetAsync(url, cancellationToken)
            .ConfigureAwait(false);

        responseMessage.EnsureSuccessStatusCodeEx();

        var servicesMap = await responseMessage
            .DeserializeJsonContentAsync<Dictionary<string, object>>(cancellationToken)
            .ConfigureAwait(false);

        return servicesMap?.ContainsKey(serviceId) == true;
    }

    public async Task SendServiceCheckTtlAsync(string checkId, string note, CancellationToken cancellationToken)
    {
        checkId.EnsureIsServiceIdValid();

        var url = consulEndpoint.GetConfig().GetAgentApiMethodUrl($"check/update/{checkId}");

        using var content = new TtlCheckRequestBody(note)
            .CreateHttpJsonStringContentWrapper(mustDisposeRequestHttpContent);

        using var responseMessage = await httpClientRef
            .PutAsync(url, content.HttpContent, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        responseMessage.EnsureSuccessStatusCodeEx();
    }
}
