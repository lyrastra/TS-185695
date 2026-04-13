using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.Alfa;
using Moedelo.BankIntegrations.ApiClient.Dto.Alfa;
using Moedelo.BankIntegrations.ApiClient.Dto.Common;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Dto.Movements;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.ApiClient.Alfa
{
    [InjectAsSingleton(typeof(IAlfaAdapterClient))]
    internal class AlfaAdapterClient : BaseApiClient, IAlfaAdapterClient
    {
        public AlfaAdapterClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<AlfaAdapterClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("AlfaAdapterEndpoint"),
                logger)
        {
        }
        
        public async Task<OpenIdGetTokenResponseDto> RequestTokensAsync(RequestTokensRequestDto requestDto)
        {
            var response = await PostAsync<RequestTokensRequestDto, ApiDataResult<OpenIdGetTokenResponseDto>>(
                uri: "/OAuth/RequestTokens",
                data: requestDto);

            return response.data;
        }

        public async Task<ClientInfoResponseDto> GetClientInfoAsync(string accessToken)
        {
            var response = await GetAsync<ApiDataResult<ClientInfoResponseDto>>(
                uri: "/Sso/GetClientInfo",
                queryParams: new { accessToken });
            return response.data;
        }
        
        public async Task<UserInfoResponseDto> GetUserInfoAsync(string accessToken)
        {
            var response = await GetAsync<ApiDataResult<UserInfoResponseDto>>(
                uri: "/Sso/GetUserInfo",
                queryParams: new { accessToken });
            return response.data;
        }

        public async Task<RequestMovementResponseDto> RequestMovementsAsync(RequestMovementRequestDto requestDto)
        {
            var response = await PostAsync<RequestMovementRequestDto, ApiDataResult<RequestMovementResponseDto>>(
                uri: "/BankOperation/RequestMovements",
                data: requestDto);

            return response.data;
        }
        
        public async Task<List<AccountResponseDto>> ValidAccountsAsync(ValidAccountsRequestDto requestDto, CancellationToken cancellationToken)
        {
            var response = await PostAsync<ValidAccountsRequestDto, ApiDataResult<List<AccountResponseDto>>>(
                uri: "/Accounts/Valid",
                data: requestDto, cancellationToken: cancellationToken);

            return response.data;
        }
        
        public async Task<bool> UpdateIntegrationDataAsync(IntegrationDataRequestDto requestDto, CancellationToken cancellationToken)
        {
            var response = await PutAsync<IntegrationDataRequestDto, ApiDataResult<bool>>(
                uri: "/Sso/UpdateIntegrationData",
                data: requestDto, cancellationToken: cancellationToken);

            return response.data;
        }
    }
}
