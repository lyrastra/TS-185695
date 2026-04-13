using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.PaymentHistory;
using Moedelo.Billing.Abstractions.PaymentHistory;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using InfrastrcutureV2HttpQuerySetting
    = Moedelo.InfrastructureV2.Domain.Models.ApiClient.HttpQuerySetting;

namespace Moedelo.Billing.ApiClient.NetFramework.PaymentHistory;

[InjectAsSingleton(typeof(IPaymentHistoryV3ApiClient))]
public class PaymentHistoryV3ApiClient(
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
        auditScopeManager), IPaymentHistoryV3ApiClient
{
    private readonly SettingValue apiEndpoint
        = settingRepository.Get("PaymentHistoryApiEndpoint");

    public Task<List<PaymentHistoryDto>> GetByCriteriaAsync(
        PaymentHistoryRequestDto criteria, HttpQuerySetting setting = null)
    {
        const string uri = "/v3/PaymentHistory/ByCriteria";

        return PostAsync<PaymentHistoryRequestDto, List<PaymentHistoryDto>>(
            uri, criteria, setting: BuildHttpQuerySetting(setting));
    }

    public Task<PaymentHistoryDto> GetByIdAsync(int paymentHistoryId)
    {
        const string uri = "/v3/PaymentHistory/{0}";

        return GetAsync<PaymentHistoryDto>(string.Format(uri, paymentHistoryId));
    }

    public Task<Dictionary<int, List<PaymentHistoryPositionDto>>> GetPaymentHistoriesPositionsAsync(
        IReadOnlyCollection<int> paymentHistoryIds)
    {
        const string uri = "/v3/PaymentHistory/positions";

        return PostAsync<IReadOnlyCollection<int>, Dictionary<int, List<PaymentHistoryPositionDto>>>(
            uri, paymentHistoryIds);
    }

    public Task<List<PaymentHistoryPositionDto>> GetPositionsByIdAsync(int paymentHistoryId)
    {
        const string uri = "/v3/PaymentHistory/{0}/positions";

        return GetAsync<List<PaymentHistoryPositionDto>>(string.Format(uri, paymentHistoryId));
    }

    public Task<List<int>> GetPaidFirmIdsByCriteriaAsync(PaymentHistoryPaidFirmIdsRequestDto criteria)
    {
        const string uri = "/v3/PaymentHistory/getPaidFirmIdsByCriteria";

        return PostAsync<PaymentHistoryPaidFirmIdsRequestDto, List<int>>(uri, criteria);
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
