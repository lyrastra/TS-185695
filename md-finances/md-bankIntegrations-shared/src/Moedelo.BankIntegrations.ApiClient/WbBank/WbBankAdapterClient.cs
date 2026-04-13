using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.WbBank;
using Moedelo.BankIntegrations.ApiClient.Dto.WbBank;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Dto.Movements;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.WbBank;

[InjectAsSingleton(typeof(IWbBankAdapterClient))]
public class WbBankAdapterClient : BaseApiClient, IWbBankAdapterClient
{
    public WbBankAdapterClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<WbBankAdapterClient> logger) : base(
        httpRequestExecuter,
        uriCreator,
        auditTracer,
        authHeadersGetter,
        auditHeadersGetter,
        settingRepository.Get("WbBankAdapterEndpoint"),
        logger)
    {
    }

    public async Task<RequestMovementResponseDto> RequestMovementListAsync(
        RequestMovementRequestDto requestDto,
        CancellationToken cancellationToken = default)
    {
        var response = await PostAsync<RequestMovementRequestDto, ApiDataResult<RequestMovementResponseDto>>(
            uri: "/BankOperation/RequestMovements",
            data: requestDto,
            cancellationToken: cancellationToken);

        return response.data;
    }

    public async Task<TokenResponseDto> GetTokenByCodeAsync(
        string code,
        string redirectUri,
        CancellationToken cancellationToken = default)
    {
        var response = await GetAsync<ApiDataResult<TokenResponseDto>>(
            uri: "/sso/GetTokenByCode",
            queryParams: new { code, redirectUri },
            cancellationToken: cancellationToken);

        return response.data;
    }

    public async Task<ClientInfoResponseDto> GetClientInfoAsync(
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        var response = await GetAsync<ApiDataResult<ClientInfoResponseDto>>(
            uri: "/sso/GetClientInfo",
            queryParams: new { accessToken },
            cancellationToken: cancellationToken);

        return response.data;
    }
}
