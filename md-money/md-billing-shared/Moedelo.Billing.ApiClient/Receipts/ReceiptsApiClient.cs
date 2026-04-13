using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Receipts;
using Moedelo.Billing.Abstractions.Receipts.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients.Receipts;

[InjectAsSingleton(typeof(IReceiptsApiClient))]
public class ReceiptsApiClient(
    IHttpRequestExecuter httpRequestExecutor,
    IUriCreator uriCreator,
    IAuditTracer auditTracer,
    IAuthHeadersGetter authHeadersGetter,
    IAuditHeadersGetter auditHeadersGetter,
    ISettingRepository settingRepository,
    ILogger<ReceiptsApiClient> logger)
    : BaseApiClient(httpRequestExecutor,
        uriCreator,
        auditTracer,
        authHeadersGetter,
        auditHeadersGetter,
        settingRepository.Get("BillingReceiptsApiEndpoint"),
        logger), IReceiptsApiClient
{
    public Task SendAsync(ReceiptSendRequestDto requestDto)
    {
        return PostAsync("/v1/receipt/send", requestDto);
    }
}