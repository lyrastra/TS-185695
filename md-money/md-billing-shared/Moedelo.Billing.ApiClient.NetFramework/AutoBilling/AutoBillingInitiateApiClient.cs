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

[InjectAsSingleton(typeof(IAutoBillingInitiateApiClient))]
public class AutoBillingInitiateApiClient : BaseApiClient, IAutoBillingInitiateApiClient
{
    private readonly SettingValue apiEndPoint;

    public AutoBillingInitiateApiClient(
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

    public Task<GetResultDto<InitiateDto>> GetAsync(GetInitiatesRequestDto requestDto)
    {
        const string uri = "/v1/initiate/get";

        return PostAsync<GetInitiatesRequestDto, GetResultDto<InitiateDto>>(uri, requestDto);
    }

    public Task<InitiateDto> StartManualInitiateAsync(StartManualInitiateRequestDto requestDto)
    {
        const string uri = "/v1/initiate/startManualInitiate";

        return PostAsync<StartManualInitiateRequestDto, InitiateDto>(uri, requestDto);
    }

    public Task AddRequestsIntoInitiateAsync(AddRequestsIntoInitiateRequestDto requestDto)
    {
        const string uri = "/v1/initiate/addRequestsIntoInitiate";

        return PostAsync(uri, requestDto);
    }

    public Task<InitiateDto> SetStateAsync(SetInitiateStateRequestDto requestDto)
    {
        const string uri = "/v1/initiate/setState";

        return PostAsync<SetInitiateStateRequestDto, InitiateDto>(uri, requestDto);
    }

    public Task<InitiateDto> SetNextStateAsync(SetNextInitiateStateRequestDto requestDto)
    {
        const string uri = "/v1/initiate/setNextState";

        return PostAsync<SetNextInitiateStateRequestDto, InitiateDto>(uri, requestDto);
    }

    public Task<InitiateDto> SetCancelStateAsync(SetCancelInitiateStateRequestDto requestDto)
    {
        const string uri = "/v1/initiate/setCancelState";

        return PostAsync<SetCancelInitiateStateRequestDto, InitiateDto>(uri, requestDto);
    }

    protected override string GetApiEndpoint()
    {
        return apiEndPoint.Value;
    }
}