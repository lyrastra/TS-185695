using System;
using Moedelo.Billing.Abstractions.YaPay;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.YaPay;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Billing.ApiClient.NetFramework.YaPay;

[InjectAsSingleton(typeof(IYaPayOrdersApiClient))]
public class YaPayOrdersApiClient : BaseApiClient, IYaPayOrdersApiClient
{
    private readonly SettingValue apiEndpoint;

    public YaPayOrdersApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager,
        ISettingRepository settingRepository)
        : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
    {
        apiEndpoint = settingRepository.Get("BillingYaPayApiEndpoint");
    }
    protected override string GetApiEndpoint()
    {
        return apiEndpoint.Value;
    }

    public async Task<YaPayOrderCreationResponseDto> CreateOrderByBillNumberAsync(YaPayOrderCreationRequestDto dto)
    {
        var settings = new HttpQuerySetting(TimeSpan.FromMinutes(1));
        var result = await PostAsync<YaPayOrderCreationRequestDto,YaPayOrderCreationResponseDto>($"/v1/orders/createByBillNumber", dto, setting: settings)
            .ConfigureAwait(false);
        return result;
    }

    public Task<YaPayOrderDto> GetOrderByGuidAsync(Guid orderGuid)
    {
        var uri = $"/v1/orders/getByGuid?guid={orderGuid}";

        return GetAsync<YaPayOrderDto>(uri);
    }
}