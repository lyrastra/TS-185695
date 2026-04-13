using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.PaymentLink;
using Moedelo.Billing.Abstractions.PaymentLink.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients.PaymentLink;

[InjectAsSingleton(typeof(IBillingPaymentLinkApiClient))]
public class BillingPaymentLinkApiClient(
IHttpRequestExecuter httpRequestExecutor,
IUriCreator uriCreator,
IAuditTracer auditTracer,
IAuthHeadersGetter authHeaderGetter,
IAuditHeadersGetter auditHeadersGetter,
ISettingRepository settingRepository,
ILogger<BillingPaymentLinkApiClient> logger) : BaseApiClient(
    httpRequestExecutor,
    uriCreator,
    auditTracer,
    authHeaderGetter,
    auditHeadersGetter,
    settingRepository.Get("BillingBillsApiEndpoint"),
    logger), IBillingPaymentLinkApiClient
{
    public Task<PaymentLinkDto> GetPaymentLinkInfoByGuidAsync(string linkId)
    {
        const string uri = "/v1/paymentLink";

        return GetAsync<PaymentLinkDto>(uri, new { linkId });
    }

    public Task<PaymentLinkDto> GetPaymentLinkInfoByPaymentHistoryIdAsync(int paymentHistoryId)
    {
        const string uri = "/v1/paymentLink/getById";

        return GetAsync<PaymentLinkDto>(uri, new { paymentHistoryId });
    }

    public Task CreateNewGuidEntryAsync(PaymentLinkRequestDto request)
    {
        const string uri = "/v1/paymentLink/create";

        return PostAsync(uri, request);
    }

    public Task<List<PaymentLinkDto>> GetPaymentLinkInfoByPaymentHistoryIdsAsync(IReadOnlyCollection<int> paymentHistoryIds)
    {
        const string uri = "/v1/paymentLink/getByIds";

        return PostAsync<IReadOnlyCollection<int>, List<PaymentLinkDto>>(uri, paymentHistoryIds);
    }
}
