using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Dto.PaymentHistory;
using Moedelo.Billing.Abstractions.PaymentHistory;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Billing.Clients.PaymentHistory;

[InjectAsSingleton(typeof(IPaymentHistoryV3ApiClient))]
public class PaymentHistoryV3ApiClient(
    IHttpRequestExecuter httpRequestExecutor,
    IUriCreator uriCreator,
    IAuditTracer auditTracer,
    IAuthHeadersGetter authHeadersGetter,
    IAuditHeadersGetter auditHeadersGetter,
    ISettingRepository settingRepository,
    ILogger<PaymentHistoryV3ApiClient> logger) :
    BaseApiClient(
        httpRequestExecutor,
        uriCreator,
        auditTracer,
        authHeadersGetter,
        auditHeadersGetter,
        settingRepository.Get("PaymentHistoryApiEndpoint"),
        logger), IPaymentHistoryV3ApiClient
{
    public Task<List<PaymentHistoryDto>> GetByCriteriaAsync(PaymentHistoryRequestDto criteria,
        HttpQuerySetting setting = null)
    {
        const string uri = "/v3/PaymentHistory/ByCriteria";

        return PostAsync<PaymentHistoryRequestDto, List<PaymentHistoryDto>>(uri, criteria, setting: setting);
    }

    public Task<PaymentHistoryDto> GetByIdAsync(int paymentHistoryId)
    {
        const string uri = "/v3/PaymentHistory/{0}";

        return GetAsync<PaymentHistoryDto>(string.Format(uri, paymentHistoryId));
    }

    public Task<List<PaymentHistoryPositionDto>> GetPositionsByIdAsync(int paymentHistoryId)
    {
        const string uri = "/v3/PaymentHistory/{0}/positions";

        return GetAsync<List<PaymentHistoryPositionDto>>(string.Format(uri, paymentHistoryId));
    }

    public Task<Dictionary<int, List<PaymentHistoryPositionDto>>> GetPaymentHistoriesPositionsAsync(
        IReadOnlyCollection<int> paymentHistoryIds)
    {
        const string uri = "/v3/PaymentHistory/positions";

        return PostAsync<IReadOnlyCollection<int>, Dictionary<int, List<PaymentHistoryPositionDto>>>(
            uri, paymentHistoryIds);
    }

    public Task<List<int>> GetPaidFirmIdsByCriteriaAsync(PaymentHistoryPaidFirmIdsRequestDto criteria)
    {
        const string uri = "/v3/PaymentHistory/getPaidFirmIdsByCriteria";
        
        return PostAsync<PaymentHistoryPaidFirmIdsRequestDto, List<int>>(uri, criteria);
    }
}
