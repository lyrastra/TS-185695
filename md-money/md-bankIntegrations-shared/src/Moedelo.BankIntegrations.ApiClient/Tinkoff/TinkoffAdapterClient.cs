using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.Tinkoff;
using Moedelo.BankIntegrations.ApiClient.Dto.Accounts;
using Moedelo.BankIntegrations.ApiClient.Dto.Tinkoff;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Dto.Movements;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.BankIntegrations.ApiClient.Tinkoff
{
    [InjectAsSingleton]
    public class TinkoffAdapterClient : BaseApiClient, ITinkoffAdapterClient
    {
        public TinkoffAdapterClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<TinkoffAdapterClient> logger) : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("TinkoffbankAdapterEndpoint"),
                logger)
        {
        }

        public async Task<TinkoffClientInfoDto> GetClientInfoAsync(string redirectUri, string code)
        {
            var response = await GetAsync<ApiDataResult<TinkoffClientInfoDto>>(
             uri: "/sso/GetClientInfo",
             queryParams: new { redirectUri, code },
             setting: new HttpQuerySetting(TimeSpan.FromMinutes(1)));

            return response.data;
        }

        public async Task<GetAccountsResponseDto> GetAccounts(int firmId)
        {
            var response = await GetAsync<ApiDataResult<GetAccountsResponseDto>>(
            uri: "/BankOperation/GetAccounts",
            queryParams: new { firmId },
            setting: new HttpQuerySetting(TimeSpan.FromMinutes(1)));

            return response.data;
        }

        public async Task<int> SaveIntegrationDataAsync(IntegrationDataDto integrationDataDto)
        {
            var response = await PutAsync<IntegrationDataDto, ApiDataResult<int>>(
                uri: "/sso/SaveIntegrationData",
                data: integrationDataDto);

            return response.data;
        }

        public async Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto)
        {
            var response = await PostAsync<RequestMovementRequestDto, ApiDataResult<RequestMovementResponseDto>>(
                uri: "/BankOperation/RequestMovements",
                data: requestDto);

            return response.data;
        }

        public async Task<RequestMovementResponseDto> MonitoringRequestMovementAsync(RequestMovementRequestDto request)
        {
            var response = await PostAsync<RequestMovementRequestDto, ApiDataResult<RequestMovementResponseDto>>(
                uri: "/Monitoring/RequestMovement",
                data: request);

            return response.data;
        }
    }
}
