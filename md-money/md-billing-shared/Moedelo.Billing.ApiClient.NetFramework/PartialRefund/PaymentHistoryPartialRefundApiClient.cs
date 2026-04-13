using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.PartialRefund;
using Moedelo.Billing.Abstractions.PartialRefund;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Billing.ApiClient.NetFramework.PartialRefund;

[InjectAsSingleton(typeof(IPaymentHistoryPartialRefundApiClient))]
public class PaymentHistoryPartialRefundApiClient(
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
        auditScopeManager), IPaymentHistoryPartialRefundApiClient
{
    private readonly SettingValue apiEndpoint = settingRepository.Get("PaymentHistoryApiEndpoint");

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
        
    public Task<IReadOnlyCollection<PaymentHistoryPartialRefundDto>> GetByPaymentHistoryIdsAsync(IReadOnlyCollection<int> paymentHistoryIds)
    {
        const string uri = "/v1/PartialRefund/GetByPaymentHistoryIds";

        return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<PaymentHistoryPartialRefundDto>>(uri, paymentHistoryIds);
    }

    protected override string GetApiEndpoint()
    {
        return apiEndpoint.Value;
    }
}