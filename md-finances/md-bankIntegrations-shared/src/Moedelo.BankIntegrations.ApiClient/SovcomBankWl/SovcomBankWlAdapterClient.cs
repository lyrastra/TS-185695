using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.SovcombankWl;
using Moedelo.BankIntegrations.ApiClient.Sovcom;
using Moedelo.BankIntegrations.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.SovcomBankWl;
using Moedelo.BankIntegrations.Dto.Movements;

namespace Moedelo.BankIntegrations.ApiClient.SovcomBankWl;

[InjectAsSingleton]
public class SovcomBankWlAdapterClient : BaseApiClient, ISovcomBankWlAdapterClient
{
    public SovcomBankWlAdapterClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<SovcomBankAdapterClient> logger) : base(
            httpRequestExecuter,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("SovcomBankWlApiAdapterEndpoint"),
            logger)
    {
    }

    public async Task<ClientInfoResponseDto> GetClientInfoAsync(string clientHashId, string state, HttpQuerySetting setting = null)
    {
        var result = await GetAsync<ApiDataResult<ClientInfoResponseDto>>(
            uri: "/sso/GetClientInfo",
            queryParams: new { clientHashId, state },
            setting: setting);
        return result.data;
    }

    public async Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto)
    {
        var response = await PostAsync<RequestMovementRequestDto, ApiDataResult<RequestMovementResponseDto>>(
            uri: "/BankOperation/RequestMovements",
            data: requestDto);

        return response.data;
    }

    public async Task<SubscribeStatusResponseDto> SetSubscribeStatusAsync(SubscribeStatusRequestDto subscribeStatusRequest, HttpQuerySetting setting = null)
    {
        var response = await PostAsync<SubscribeStatusRequestDto, ApiDataResult<SubscribeStatusResponseDto>>(
            uri: "/sso/SetSubscribeStatus",
            data: subscribeStatusRequest,
            setting: setting);

        return response.data;
    }
}
