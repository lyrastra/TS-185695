using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Dto.PartialRefund;
using Moedelo.Billing.Abstractions.PartialRefund;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Threading.Tasks;

namespace Moedelo.Billing.Clients.PartialRefund;

[InjectAsSingleton(typeof(IPaymentHistoryPartialRefundApiClient))]
public class PaymentHistoryPartialRefundApiClient(
    IHttpRequestExecuter httpRequestExecutor,
    IUriCreator uriCreator,
    IAuditTracer auditTracer,
    IAuthHeadersGetter authHeadersGetter,
    IAuditHeadersGetter auditHeadersGetter,
    ISettingRepository settingRepository,
    ILogger<PaymentHistoryPartialRefundApiClient> logger) :
    BaseApiClient(
        httpRequestExecutor,
        uriCreator,
        auditTracer,
        authHeadersGetter,
        auditHeadersGetter,
        settingRepository.Get("PaymentHistoryApiEndpoint"),
        logger), IPaymentHistoryPartialRefundApiClient
{
    public Task<PaymentHistoryPartialRefundDto> GetByPaymentHistoryIdAsync(int paymentHistoryId)
    {
        const string uri = "/v1/PartialRefund/ByPaymentHistoryId/{0}";

        return GetAsync<PaymentHistoryPartialRefundDto>(string.Format(uri, paymentHistoryId));
    }

    public Task SaveAsync(PaymentHistoryPartialRefundDto dto)
    {
        const string uri = "/v1/PartialRefund/Save";

        return PostAsync(uri, dto);
    }

    public Task<IReadOnlyCollection<PaymentHistoryPartialRefundDto>> GetByPaymentHistoryIdsAsync(
        IReadOnlyCollection<int> paymentHistoryIds)
    {
        const string uri = "/v1/PartialRefund/GetByPaymentHistoryIds";

        return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<PaymentHistoryPartialRefundDto>>(uri,
            paymentHistoryIds);
    }
}