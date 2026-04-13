using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.BankIntegrationsNetCore;
using Moedelo.BankIntegrations.ApiClient.Dto.InvoicePaymentOrder;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.ApiClient.BankIntegrationsNetCore;

[InjectAsSingleton(typeof(IInvoicePaymentOrderApiClient))]
public class InvoicePaymentOrderApiClient : BaseApiClient, IInvoicePaymentOrderApiClient
{
    public InvoicePaymentOrderApiClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<InvoicePaymentOrderApiClient> logger)
        : base(
            httpRequestExecuter,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("IntegrationApiNetCore"),
            logger)
    {
    }
        
    public async Task<bool> SyncDetailAsync(InvoiceSyncDetailRequestDto requestDto, CancellationToken ct)
    {
        return await PostAsync<InvoiceSyncDetailRequestDto, bool>("/private/api/v1/invoicePaymentOrder/syncDetail", requestDto, cancellationToken: ct);
    }
}