using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Dto.PaymentCategory;
using Moedelo.Billing.Abstractions.PaymentCategory;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients.PaymentCategory;

[InjectAsSingleton(typeof(IPaymentCategoryApiClient))]
public class PaymentCategoryApiClient(IHttpRequestExecuter httpRequestExecutor,
    IUriCreator uriCreator,
    IAuditTracer auditTracer,
    IAuthHeadersGetter authHeadersGetter,
    IAuditHeadersGetter auditHeadersGetter,
    ISettingRepository settingRepository,
    ILogger<PaymentCategoryApiClient> logger) :
    BaseApiClient(
        httpRequestExecutor,
        uriCreator,
        auditTracer,
        authHeadersGetter,
        auditHeadersGetter,
        settingRepository.Get("PaymentHistoryApiEndpoint"),
        logger), IPaymentCategoryApiClient
{
    public Task<IReadOnlyCollection<PaymentCategoryDto>> GetByPaymentHistoryIdsAsync(IReadOnlyCollection<int> paymentHistoryIds)
    {
        const string uri = "/v1/PaymentCategory/GetByPaymentHistoryIds";

        return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<PaymentCategoryDto>>(uri, paymentHistoryIds);
    }

    public Task InsertNoteAsync(PaymentCategoryDto dto)
    {
        const string uri = "/v1/PaymentCategory/InsertNote";

        return PostAsync(uri, dto);
    }
}