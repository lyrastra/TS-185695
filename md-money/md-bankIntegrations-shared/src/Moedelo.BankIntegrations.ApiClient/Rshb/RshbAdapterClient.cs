using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.Rshb;
using Moedelo.BankIntegrations.ApiClient.Dto.Rshb;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Dto.Movements;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.ApiClient.Rshb;

[InjectAsSingleton(typeof(IRshbAdapterClient))]
public class RshbAdapterClient : BaseApiClient, IRshbAdapterClient
{
    public RshbAdapterClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<RshbAdapterClient> logger)
        : base(
            httpRequestExecuter,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("RshbBankAdapterEndpoint"),
            logger)
    {
    }
    
    public async Task<ClientInfoResponseDto> GetClientInfoAsync(string rshbOrgId)
    {
        var response = await GetAsync<ApiDataResult<ClientInfoResponseDto>>(
            uri: "/Sso/GetClientInfo",
            queryParams: new { rshbOrgId });
        return response.data;
    }
    
    public async Task<bool> SendConfirmAsync(SendConfirmRequestDto dto)
    {
        var response = await PostAsync<SendConfirmRequestDto, ApiDataResult<bool>>(
            uri: "/Sso/SendConfirm",
            data: dto);
        return response.data;
    }
    
    public async Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto)
    {
        var response = await PostAsync<RequestMovementRequestDto, ApiDataResult<RequestMovementResponseDto>>(
            uri: "/BankOperation/RequestMovements",
            data: requestDto);

        return response.data;
    }
}