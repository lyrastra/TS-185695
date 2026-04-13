using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.AutoBilling;
using Moedelo.Billing.Abstractions.AutoBilling.Dto;
using Moedelo.Billing.Abstractions.AutoBilling.ResultDto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients.AutoBilling;

[InjectAsSingleton(typeof(IAutoBillingInitiateApiClient))]
public sealed class AutoBillingInitiateApiClient : BaseApiClient, IAutoBillingInitiateApiClient
{
    public AutoBillingInitiateApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeaderGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<AutoBillingInitiateApiClient> logger)
        : base(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            authHeaderGetter,
            auditHeadersGetter,
            settingRepository.Get("AutoBillingApiEndpoint"),
            logger)
    {
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
}