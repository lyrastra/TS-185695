using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Extensions;
using Moedelo.InfrastructureV2.Domain.AuditModule.Helpers;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.AuditWebApi.Handlers;

[InjectAsSingleton(typeof(AuditTrailHeaderInjectionHandler))]
public sealed class AuditTrailHeaderInjectionHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(true);
        
        // Извлекаем span и добавляем заголовки
        if (request.Properties.TryGetValue(AuditHttpRequestProperties.AuditTrailSpan, out var spanObj) 
            && spanObj is IAuditSpan span)
        {
            response.AddAuditTrailResponseHeader(span);
        }

        return response;
    }
}
