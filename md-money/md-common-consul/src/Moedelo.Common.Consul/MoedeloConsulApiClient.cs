using System.Diagnostics;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;
using Moedelo.Common.Consul.Abstractions;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.Consul.Abstraction.Models;
using Moedelo.Infrastructure.Consul.Extensions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.Consul;

[InjectAsSingleton(typeof(IMoedeloConsulApiClient))]
internal sealed class MoedeloConsulApiClient : IMoedeloConsulApiClient
{
    private readonly IConsulHttpApiClient apiClient;
    private readonly IAuditTracer auditTracer;

    public MoedeloConsulApiClient(
        IConsulHttpApiClient apiClient,
        IAuditTracer auditTracer)
    {
        this.apiClient = apiClient;
        this.auditTracer = auditTracer;
    }

    public async ValueTask<HttpResponseMessage> GetAsync(string keyPath, string uriQuery,
        CancellationToken cancellationToken, string? auditTrailSpanNameSuffix = null)
    {
        const string auditTrailSpanNameBase = $"{nameof(MoedeloConsulApiClient)}.Get";
        var auditTrailSpanName = BuildAuditTrailSpanName(auditTrailSpanNameBase, auditTrailSpanNameSuffix);

        using var scope = auditTracer
            .BuildSpan(AuditSpanType.ConsulHttpCall, auditTrailSpanName)
            .WithTag("KeyPath", keyPath)
            .Start();

        try
        {
            return await apiClient.GetAsync(keyPath, uriQuery, cancellationToken);
        }
        catch (Exception exception)
        {
            scope.Span.SetError(exception);
            throw;
        }
    }

    public async ValueTask SaveKeyValueAsync(string keyPath, string value, CancellationToken cancellationToken,
        string? auditTrailSpanNameSuffix = default)
    {
        const string auditTrailSpanNameBase = $"{nameof(MoedeloConsulApiClient)}.Set";
        var auditTrailSpanName = BuildAuditTrailSpanName(auditTrailSpanNameBase, auditTrailSpanNameSuffix);

        using var scope = auditTracer
            .BuildSpan(AuditSpanType.ConsulHttpCall, auditTrailSpanName)
            .WithTag("KeyPath", keyPath)
            .Start();

        try
        {
            await apiClient.SaveKeyValueAsync(keyPath, value, cancellationToken);
        }
        catch (Exception exception)
        {
            scope.Span.SetError(exception);
            throw;
        }
    }

    public ValueTask SaveKeyJsonValueAsync<TValue>(string keyPath, TValue value, CancellationToken cancellationToken,
        string? auditTrailSpanNameSuffix = null)
    {
        return SaveKeyValueAsync(keyPath, value.ToJsonString(), cancellationToken, auditTrailSpanNameSuffix);
    }

    public async ValueTask<ConsulKeyValue<TValue>[]> ListKeysAsAsync<TValue>(string keyPath, CancellationToken cancellationToken,
        string? auditTrailSpanNameSuffix = default)
    {
        const string auditTrailSpanNameBase = $"{nameof(MoedeloConsulApiClient)}.List";
        var auditTrailSpanName = BuildAuditTrailSpanName(auditTrailSpanNameBase, auditTrailSpanNameSuffix);
        
        using var scope = auditTracer
            .BuildSpan(AuditSpanType.ConsulHttpCall, auditTrailSpanName)
            .WithTag("KeyPath", keyPath)
            .Start();

        try
        {
            var keys = await apiClient.ListKeysAsAsync<TValue>(keyPath, cancellationToken);
            
            scope.Span.AddTag("Keys.Count", keys.Length);

            return keys;
        }
        catch (Exception exception)
        {
            scope.Span.SetError(exception);
            throw;
        }
    }

    public ValueTask DeleteKeysByPrefixAsync(string keyPrefix, CancellationToken cancellationToken)
    {
        return DoDeleteKeysByPrefixAsync(keyPrefix, cancellationToken, null);
    }

    public ValueTask DeleteKeysByPrefixAsync(string keyPrefix, CancellationToken cancellationToken,
        string auditTrailSpanNameSuffix)
    {
        Debug.Assert(string.IsNullOrEmpty(auditTrailSpanNameSuffix) == false);

        return DoDeleteKeysByPrefixAsync(keyPrefix, cancellationToken, auditTrailSpanNameSuffix);
    }

    private async ValueTask DoDeleteKeysByPrefixAsync(string keyPrefix, CancellationToken cancellationToken,
        string? auditTrailSpanNameSuffix = null)
    {
        const string auditTrailSpanNameBase = $"{nameof(MoedeloConsulApiClient)}.DeleteByPrefix";
        var auditTrailSpanName = BuildAuditTrailSpanName(auditTrailSpanNameBase, auditTrailSpanNameSuffix);

        using var scope = auditTracer
            .BuildSpan(AuditSpanType.ConsulHttpCall, auditTrailSpanName)
            .WithTag("KeyPathPrefix", keyPrefix)
            .Start();

        try
        {
            await apiClient.DeleteKeysByPrefixAsync(keyPrefix, cancellationToken);
        }
        catch (Exception exception)
        {
            scope.Span.SetError(exception);
            throw;
        }
    }
    
    private static string BuildAuditTrailSpanName(string baseName, string? suffix)
    {
        return string.IsNullOrEmpty(suffix)
            ? baseName
            : $"{baseName}.{suffix}";
    }

}
