using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.OtpBank;
using Moedelo.BankIntegrations.ApiClient.Dto.OtpBank;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Dto.Movements;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using System;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.OtpBank;

[InjectAsSingleton(typeof(IOtpAdapterClient))]
internal class OtpAdapterClient : BaseApiClient, IOtpAdapterClient
{
    public OtpAdapterClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<OtpAdapterClient> logger)
        : base(
            httpRequestExecuter,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("OtpBankAdapterEndpoint"),
            logger)
    {
    }

    public async Task<ClientInfoResponseDto> GetClientInfoAsync(string integrationId)
    {
        var response = await GetAsync<ApiDataResult<ClientInfoResponseDto>>(
            uri: "/Sso/GetClientInfo",
            queryParams: new { integrationId });

        return response.data;
    }

    public async Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto)
    {
        var response = await PostAsync<RequestMovementRequestDto, ApiDataResult<RequestMovementResponseDto>>(
            uri: "/BankOperation/RequestMovements",
            data: requestDto,
            setting: new HttpQuerySetting(TimeSpan.FromMinutes(2)));

        return response.data;
    }
}
