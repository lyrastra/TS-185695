using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.AutoBilling;
using Moedelo.Billing.Abstractions.AutoBilling.Dto.System;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Billing.ApiClient.NetFramework.AutoBilling;

[InjectAsSingleton(typeof(IAutoBillingSystemApiClient))]
public class AutoBillingSystemApiClient : BaseApiClient, IAutoBillingSystemApiClient
{
    private readonly SettingValue apiEndPoint;

    public AutoBillingSystemApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager)
        : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
    {
        apiEndPoint = settingRepository.Get("AutoBillingApiEndpoint");
    }

    public Task<AutoBillingSystemDto> GetAsync(AutoBillingSystemRequestDto dto)
    {
        const string uri = "/v1/system";

        return GetAsync<AutoBillingSystemDto>(uri, dto);
    }

    public Task<AutoBillingSystemDto> SetAsync(SetAutoBillingSystemRequestDto requestDto)
    {
        const string uri = "/v1/system";

        return PostAsync<SetAutoBillingSystemRequestDto, AutoBillingSystemDto>(uri, requestDto);
    }

    protected override string GetApiEndpoint()
    {
        return apiEndPoint.Value;
    }
}