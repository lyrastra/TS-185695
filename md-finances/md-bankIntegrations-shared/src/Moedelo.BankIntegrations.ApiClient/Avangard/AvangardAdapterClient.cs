using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.Avangard;
using Moedelo.BankIntegrations.ApiClient.Dto.Avangard;
using Moedelo.BankIntegrations.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using System;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.Dto.Movements;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Avangard
{
    [InjectAsSingleton]
    public class AvangardAdapterClient : BaseApiClient, IAvangardAdapterClient
    {
        public AvangardAdapterClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<AvangardAdapterClient> logger) : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("AvangardbankAdapterEndpoint"),
                logger)
        {
        }

        public async Task<TokenDto> GetTokenAsync(string code, string redirectUri)
        {
            var response = await GetAsync<ApiDataResult<TokenDto>>(
             uri: "/sso/GetToken",
             queryParams: new { redirectUri, code },
             setting: new HttpQuerySetting(TimeSpan.FromMinutes(1)));

            return response.data;
        }

        public async Task<int> SaveTokenDataAsync(TokenDataDto integrationDataDto)
        {
            var response = await PutAsync<TokenDataDto, ApiDataResult<int>>(
                uri: "/sso/SaveTokenData",
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

    }
}
