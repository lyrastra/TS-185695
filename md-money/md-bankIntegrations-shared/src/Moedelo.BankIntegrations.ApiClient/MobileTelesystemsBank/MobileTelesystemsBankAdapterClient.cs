using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.Mtsbank;
using Moedelo.BankIntegrations.ApiClient.Dto.Accounts;
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

namespace Moedelo.BankIntegrations.ApiClient.MobileTelesystemsBank
{
    [InjectAsSingleton]
    internal class MobileTelesystemsBankAdapterClient : BaseApiClient, IMobileTelesystemsBankAdapterClient
    {
        public MobileTelesystemsBankAdapterClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<MobileTelesystemsBankAdapterClient> logger) : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("MobileTelesystemsBankAdapterEndpoint"),
                logger)
        {

        }

        /// <summary>Создает заявку на подключение</summary>
        /// <returns>ApplicationId</returns>
        public async Task<string> CreateRequestOnIntegrationAsync(string inn, string login)
        {
            return (await GetAsync<ApiDataResult<string>>(
             uri: "/sso/CreateRequestOnIntegration",
             queryParams: new { inn, login })).data;
        }

        public async Task<GetAccountsResponseDto> GetAccountsAsync(string customerId)
        {
            var response = await GetAsync<ApiDataResult<GetAccountsResponseDto>>(
            uri: "/sso/GetAccounts",
            queryParams: new { customerId },
            setting: new HttpQuerySetting(TimeSpan.FromMinutes(1)));

            return response.data;
        }

        public async Task<Dto.MobileTelesystemsBank.AccountDto> GetAccountAsync(string customerId, string account)
        {
            var response = await GetAsync<ApiDataResult<Dto.MobileTelesystemsBank.AccountDto>>(
            uri: "/sso/GetAccount",
            queryParams: new { customerId, account },
            setting: new HttpQuerySetting(TimeSpan.FromMinutes(1)));

            return response.data;
        }

        public async Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto)
        {
            var response = await PostAsync<RequestMovementRequestDto, ApiDataResult<RequestMovementResponseDto>>(
                uri: "/BankOperation/RequestMovements",
                data: requestDto);

            return response.data;
        }
        
        public async Task<RequestMovementResponseDto> MonitoringRequestMovementListAsync(RequestMovementRequestDto requestDto)
        {
            var response = await PostAsync<RequestMovementRequestDto, ApiDataResult<RequestMovementResponseDto>>(
                uri: "/Monitoring/RequestMovements",
                data: requestDto);

            return response.data;
        }
    }
}