using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;
using Moedelo.Common.Consul.Abstractions;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Consul;

[InjectAsSingleton(typeof(IMoedeloConsulCatalogApiClient))]
internal sealed class MoedeloConsulCatalogApiClient : IMoedeloConsulCatalogApiClient
{
    private readonly IAuditTracer auditTracer;
    private readonly IConsulCatalogApiClient apiClient;

    public MoedeloConsulCatalogApiClient(
        IConsulHttpApiClient apiClient,
        IAuditTracer auditTracer)
    {
        this.auditTracer = auditTracer;
        this.apiClient = apiClient.CatalogApi;
    }

    public async ValueTask<IReadOnlyCollection<string>> GetServiceIdsListAsync(CancellationToken cancellationToken)
    {
        const string auditTrailSpanName = $"{nameof(MoedeloConsulCatalogApiClient)}.{nameof(GetServiceIdsListAsync)}";

        using var scope = auditTracer
            .BuildSpan(AuditSpanType.ConsulHttpCall, auditTrailSpanName)
            .Start();

        try
        {
            return await apiClient.GetServiceIdsListAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            scope.Span.SetError(exception);
            throw;
        }
    }
}
