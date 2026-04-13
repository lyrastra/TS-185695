using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.PaymentCategory;
using Moedelo.Billing.Abstractions.PaymentCategory;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Billing.ApiClient.NetFramework.PaymentCategory;

[InjectAsSingleton(typeof(IPaymentCategoryApiClient))]
public class PaymentCategoryApiClient(
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
        auditScopeManager), IPaymentCategoryApiClient
{
    
    private readonly SettingValue apiEndpoint = settingRepository.Get("PaymentHistoryApiEndpoint");
    protected override string GetApiEndpoint()
    {
        return apiEndpoint.Value;
    }

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