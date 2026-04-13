using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.PaymentHistory;
using Moedelo.Billing.Abstractions.PaymentHistory;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using InfrastrcutureV2HttpQuerySetting
    = Moedelo.InfrastructureV2.Domain.Models.ApiClient.HttpQuerySetting;

namespace Moedelo.Billing.ApiClient.NetFramework.PaymentHistory;

[InjectAsSingleton(typeof(IPaymentHistoryExV3ApiClient))]
public class PaymentHistoryExV3ApiClient(
    IHttpRequestExecutor httpRequestExecutor,
    IUriCreator uriCreator,
    IResponseParser responseParser,
    IAuditTracer auditTracer,
    IAuditScopeManager auditScopeManager,
    ISettingRepository settingRepository)
    : BaseApiClient(
        httpRequestExecutor,
        uriCreator,
        responseParser,
        auditTracer,
        auditScopeManager), IPaymentHistoryExV3ApiClient
{
    private readonly SettingValue apiEndpoint
        = settingRepository.Get("PaymentHistoryApiEndpoint");

    public Task<PaymentHistoryExDto> GetByBillNumberAsync(string billNumber, HttpQuerySetting httpQuerySetting = null)
    {
        const string uri = "/v3/PaymentHistoryEx/byBillNumber";

        var queryParams = new { billNumber };

        return GetAsync<PaymentHistoryExDto>(uri, queryParams, setting: BuildHttpQuerySetting(httpQuerySetting));
    }

    public Task<List<PaymentHistoryExDto>> GetByCriteriaAsync(
        PaymentHistoryExRequestDto criteriaDto, HttpQuerySetting setting = null)
    {
        const string uri = "/v3/PaymentHistoryEx/GetByCriteria";

        return PostAsync<PaymentHistoryExRequestDto, List<PaymentHistoryExDto>>(
            uri, criteriaDto, setting: BuildHttpQuerySetting(setting));

    }
    
    public Task<PaymentHistoryExDto> GetByPaymentIdAsync(int paymentId, HttpQuerySetting httpQuerySetting = null)
    {
        const string uri = "/v3/PaymentHistoryEx/GetByPaymentId";

        var queryParams = new { paymentId };

        return GetAsync<PaymentHistoryExDto>(uri, queryParams, setting: BuildHttpQuerySetting(httpQuerySetting));
    }

    protected override string GetApiEndpoint()
    {
        return apiEndpoint.Value;
    }

    private static InfrastrcutureV2HttpQuerySetting BuildHttpQuerySetting(
        HttpQuerySetting setting)
    {
        return setting == null
            ? null
            : new InfrastrcutureV2HttpQuerySetting
            {
                DontThrowOn404 = setting.DontThrowOn404,
                Timeout = setting.Timeout,
            };
    }    
}
