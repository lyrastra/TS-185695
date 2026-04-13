using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;
using Moedelo.Common.Consul.Abstractions;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.Consul.Abstraction.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Consul;

[InjectAsSingleton(typeof(IMoedeloConsulAgentApiClient))]
internal sealed class MoedeloConsulAgentApiClient : IMoedeloConsulAgentApiClient
{
    private readonly IAuditTracer auditTracer;
    private readonly IConsulAgentApiClient apiClient;

    public MoedeloConsulAgentApiClient(
        IConsulHttpApiClient apiClient,
        IAuditTracer auditTracer)
    {
        this.auditTracer = auditTracer;
        this.apiClient = apiClient.AgentApi;
    }

    public async ValueTask RegisterServiceAsync(AgentServiceRegistration registration, CancellationToken cancellationToken)
    {
        const string auditTrailSpanName = $"{nameof(MoedeloConsulAgentApiClient)}.RegisterService";

        using var scope = auditTracer
            .BuildSpan(AuditSpanType.ConsulHttpCall, auditTrailSpanName)
            .WithTag("Registration", registration)
            .Start();

        try
        {
            await apiClient.RegisterServiceAsync(registration, cancellationToken);
        }
        catch (Exception exception)
        {
            scope.Span.SetError(exception);
            throw;
        }
    }

    public async ValueTask DeregisterServiceAsync(string serviceId, CancellationToken cancellationToken)
    {
        const string auditTrailSpanName = $"{nameof(MoedeloConsulAgentApiClient)}.DeregisterService";

        using var scope = auditTracer
            .BuildSpan(AuditSpanType.ConsulHttpCall, auditTrailSpanName)
            .WithTag("ServiceId", serviceId)
            .Start();

        try
        {
            await apiClient.DeregisterServiceAsync(serviceId, cancellationToken);
        }
        catch (Exception exception)
        {
            scope.Span.SetError(exception);
            throw;
        }
    }

    public async ValueTask<bool> IsServiceRegisteredAsync(string serviceId, CancellationToken cancellationToken)
    {
        const string auditTrailSpanName = $"{nameof(MoedeloConsulAgentApiClient)}.IsServiceRegistered";

        using var scope = auditTracer
            .BuildSpan(AuditSpanType.ConsulHttpCall, auditTrailSpanName)
            .WithTag("ServiceId", serviceId)
            .Start();

        try
        {
            return await apiClient.IsServiceRegisteredAsync(serviceId, cancellationToken);
        }
        catch (Exception exception)
        {
            scope.Span.SetError(exception);
            throw;
        }
    }

    public async ValueTask SendServiceCheckTtlAsync(string checkId, string note, CancellationToken cancellationToken)
    {
        const string auditTrailSpanName = $"{nameof(MoedeloConsulAgentApiClient)}.SendServiceCheckTtl";

        using var scope = auditTracer
            .BuildSpan(AuditSpanType.ConsulHttpCall, auditTrailSpanName)
            .WithTag("Params", new {checkId, note})
            .Start();

        try
        {
            await apiClient.SendServiceCheckTtlAsync(checkId, note, cancellationToken);
        }
        catch (Exception exception)
        {
            scope.Span.SetError(exception);
            throw;
        }
    }
}
