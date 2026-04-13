using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;
using Moedelo.Common.Consul.Abstractions;
using Moedelo.Common.Consul.Internals;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Consul;

[InjectAsSingleton(typeof(IMoedeloConsulSessionalKeyValueApiClient))]
internal sealed class MoedeloConsulSessionalKeyValueApiClient : IMoedeloConsulSessionalKeyValueApiClient, IAsyncDisposable
{
    private readonly IAuditTracer auditTracer;
    private readonly IConsulSessionalKeyValueApi apiClient;

    public MoedeloConsulSessionalKeyValueApiClient(
        IMoedeloConsulSessionNamingStrategy sessionNamingStrategy,
        IConsulHttpApiClient apiClient,
        IAuditTracer auditTracer,
        IMoedeloConsulSessionMonitoring sessionMonitoring)
    {
        this.apiClient = apiClient.CreateSessionalKeyValueApiClient(sessionNamingStrategy, sessionMonitoring);
        this.auditTracer = auditTracer;
    }

    public string ConsulSessionId => apiClient.ConsulSessionId;

    public async ValueTask<bool> AcquireKeyValueAsync(string keyPath, string value, CancellationToken cancellationToken)
    {
        const string auditTrailSpanName = "SessionalKeyValue.AcquireKey";

        using var scope = auditTracer
            .BuildSpan(AuditSpanType.ConsulHttpCall, auditTrailSpanName)
            .Start();

        try
        {
            return await apiClient.AcquireKeyValueAsync(keyPath, value, cancellationToken);
        }
        catch (Exception exception)
        {
            scope.Span.SetError(exception);
            throw;
        }
    }

    public async ValueTask ReleaseAcquiredKeyValueAsync(string keyPath, CancellationToken cancellationToken)
    {
        const string auditTrailSpanName = "SessionalKeyValue.ReleaseKey";

        using var scope = auditTracer
            .BuildSpan(AuditSpanType.ConsulHttpCall, auditTrailSpanName)
            .Start();

        try
        {
            await apiClient.ReleaseAcquiredKeyValueAsync(keyPath, cancellationToken);
        }
        catch (Exception exception)
        {
            scope.Span.SetError(exception);
            throw;
        }
    }

    public ValueTask DisposeAsync()
    {
        return this.apiClient.DisposeAsync();
    }
}
