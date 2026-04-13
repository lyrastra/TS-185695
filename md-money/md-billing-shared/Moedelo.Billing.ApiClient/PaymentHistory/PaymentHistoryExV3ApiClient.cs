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

[InjectAsSingleton(typeof(IPaymentHistoryExV3ApiClient))]
public class PaymentHistoryExV3ApiClient(
    IHttpRequestExecuter httpRequestExecutor,
    IUriCreator uriCreator,
    IAuditTracer auditTracer,
    IAuthHeadersGetter authHeadersGetter,
    IAuditHeadersGetter auditHeadersGetter,
    ISettingRepository settingRepository,
    ILogger<PaymentHistoryExV3ApiClient> logger) :
    BaseApiClient(
        httpRequestExecutor,
        uriCreator,
        auditTracer,
        authHeadersGetter,
        auditHeadersGetter,
        settingRepository.Get("PaymentHistoryApiEndpoint"),
        logger), IPaymentHistoryExV3ApiClient
{
    public Task<PaymentHistoryExDto> GetByBillNumberAsync(
        string billNumber,
        HttpQuerySetting httpQuerySetting = null)
    {
        const string uri = "/v3/PaymentHistoryEx/byBillNumber";

        var queryParams = new { billNumber };

        return GetAsync<PaymentHistoryExDto>(uri, queryParams, setting: httpQuerySetting);
    }

    public Task<List<PaymentHistoryExDto>> GetByCriteriaAsync(
        PaymentHistoryExRequestDto criteriaDto,
        HttpQuerySetting setting = null)
    {
        const string uri = "/v3/PaymentHistoryEx/GetByCriteria";
        
        return PostAsync<PaymentHistoryExRequestDto, List<PaymentHistoryExDto>>(uri, criteriaDto, setting: setting);
    }
    
    public Task<PaymentHistoryExDto> GetByPaymentIdAsync(
        int paymentId,
        HttpQuerySetting httpQuerySetting = null)
    {
        const string uri = "/v3/PaymentHistoryEx/GetByPaymentId";

        var queryParams = new { paymentId };

        return GetAsync<PaymentHistoryExDto>(uri, queryParams, setting: httpQuerySetting);
    }
}
