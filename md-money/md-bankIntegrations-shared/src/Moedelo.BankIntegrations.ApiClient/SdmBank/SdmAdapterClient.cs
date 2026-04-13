using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.SdmBank;
using Moedelo.BankIntegrations.ApiClient.Dto.SdmBank;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Dto.Movements;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.BankIntegrations.ApiClient.SdmBank
{
    [InjectAsSingleton]
    public class SdmAdapterClient : BaseApiClient, ISdmAdapterClient
    {

        public SdmAdapterClient(
            IHttpRequestExecuter httpRequestExecuter, 
            IUriCreator uriCreator, 
            IAuditTracer auditTracer, 
            IAuthHeadersGetter authHeadersGetter, 
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository, 
            ILogger<SdmAdapterClient> logger) : base(
            httpRequestExecuter, 
            uriCreator, 
            auditTracer, 
            authHeadersGetter, 
            auditHeadersGetter,
            settingRepository.Get("SdmBankAdapterEndpoint"), 
            logger)
        {
        }

        public async Task<TokenDto> GetTokenAsync(string code)
        {
            var response = await GetAsync<ApiDataResult<TokenDto>>(
                uri: "/sso/GetToken",
                queryParams: new {code},
                setting: new HttpQuerySetting(TimeSpan.FromMinutes(1)));

            return response.data;
        }
        
        public async Task<int> SaveTokenDataAsync(IntegrationDataDto integrationDataDto)
        {
            var response = await PostAsync<IntegrationDataDto, ApiDataResult<int>>(
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
