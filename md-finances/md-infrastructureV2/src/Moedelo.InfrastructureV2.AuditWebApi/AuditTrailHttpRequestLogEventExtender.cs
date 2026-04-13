#nullable enable
using System.Collections.Generic;
using Moedelo.InfrastructureV2.AuditWebApi.Extensions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Extensions;
using Moedelo.InfrastructureV2.Domain.AuditModule.Helpers;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Http;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.InfrastructureV2.AuditWebApi;

public interface IAuditTrailHttpRequestLogEventExtender : ILogEventExtender { }

[InjectAsSingleton(typeof(IAuditTrailHttpRequestLogEventExtender))]
// ReSharper disable once UnusedType.Global
internal sealed class AuditTrailHttpRequestLogEventExtender : IAuditTrailHttpRequestLogEventExtender
{
    private readonly IDIResolver diResolver;

    public AuditTrailHttpRequestLogEventExtender(IDIResolver diResolver)
    {
        this.diResolver = diResolver;
    }

    public IEnumerable<KeyValuePair<string, object>> EnumerateLogExtraEventFields()
    {
        var httpEnv = GetHttpEnv();

        if (httpEnv?.HasHttpContext == true && httpEnv?.CurrentContext.GetHttpRequestMessage() is { } requestMessage)
        {
            if (requestMessage.Properties.TryGetValue(AuditHttpRequestProperties.AuditTrailSpan, out var spanObj)
                && spanObj is IAuditSpan span)
            {
                return span.EnumerateLogExtraEventFields();
            }
        }

        return [];
    }

    private IHttpEnviroment? GetHttpEnv()
    {
        try
        {
            return diResolver.GetInstance<IHttpEnviroment>();
        }
        catch
        {
            // это нормальная история - мы можем находиться не в скоупе http. В этом случае ничего не делаем
            return null;
        }
    }
}
