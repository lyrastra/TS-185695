using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.AutoBilling;
using Moedelo.Billing.Abstractions.AutoBilling.Dto;
using Moedelo.Billing.Abstractions.AutoBilling.ResultDto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Billing.ApiClient.NetFramework.AutoBilling;

[InjectAsSingleton(typeof(IAutoBillingRequestApiClient))]
public class AutoBillingRequestApiClient : BaseApiClient, IAutoBillingRequestApiClient
{
    private readonly SettingValue apiEndPoint;

    public AutoBillingRequestApiClient(
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

    public Task<GetResultDto<RequestDto>> GetAsync(GetRequestsRequestDto requestsRequestDto)
    {
        const string uri = "/v1/request/get";

        return PostAsync<GetRequestsRequestDto, GetResultDto<RequestDto>>(uri, requestsRequestDto);
    }

    public Task<RequestDto> SetStateAsync(SetRequestStateRequestDto requestDto)
    {
        const string uri = "/v1/request/setState";

        return PostAsync<SetRequestStateRequestDto, RequestDto>(uri, requestDto);
    }

    protected override string GetApiEndpoint()
    {
        return apiEndPoint.Value;
    }
}