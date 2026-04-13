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

[InjectAsSingleton(typeof(IAutoBillingRequestApiClient))]
public sealed class AutoBillingRequestApiClient : BaseApiClient, IAutoBillingRequestApiClient
{
    public AutoBillingRequestApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeaderGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<AutoBillingRequestApiClient> logger)
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
}