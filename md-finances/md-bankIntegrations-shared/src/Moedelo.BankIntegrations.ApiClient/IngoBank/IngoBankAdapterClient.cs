using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.IngoBank;
using Moedelo.BankIntegrations.ApiClient.Dto.IngoBank;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Dto.Movements;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.ApiClient.IngoBank;

[InjectAsSingleton(typeof(IIngoBankAdapterClient))]
public class IngoBankAdapterClient : BaseApiClient, IIngoBankAdapterClient
{

    public IngoBankAdapterClient(
        IHttpRequestExecuter httpRequestExecuter, 
        IUriCreator uriCreator, 
        IAuditTracer auditTracer, 
        IAuthHeadersGetter authHeadersGetter, 
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository, 
        ILogger<IngoBankAdapterClient> logger) : base(
        httpRequestExecuter, 
        uriCreator, 
        auditTracer, 
        authHeadersGetter, 
        auditHeadersGetter,
        settingRepository.Get("IngoBankAdapterEndpoint"), 
        logger)
    {
    }

    public async Task<TokenDto> GetTokenByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var response = await GetAsync<ApiDataResult<TokenDto>>(
            uri: "/sso/GetTokenByCode",
            queryParams: new {code}, 
            cancellationToken: cancellationToken);

        return response.data;
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
}