using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.Blancbank;
using Moedelo.BankIntegrations.ApiClient.Dto.Blancbank;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Dto.Movements;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountDto = Moedelo.BankIntegrations.ApiClient.Dto.Blancbank.AccountDto;

namespace Moedelo.BankIntegrations.ApiClient.Blancbank
{
    [InjectAsSingleton(typeof(IBlancbankAdapterClient))]
    public class BlancbankAdapterClient: BaseApiClient, IBlancbankAdapterClient
    {
        public BlancbankAdapterClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<BlancbankAdapterClient> logger) :base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("BlancbankAdapterEndpoint"),
                logger)
        {

        }

        public async Task<bool> WriteTokenByCodeAsync(int firmId, string code, string codeVerifier, string redirectUri)
        {
            var result = await PostAsync<ApiDataResult<bool>>(uri:
                $"/OAuth/WriteTokenByCode" +
                $"?firmId={firmId}" +
                $"&code={code}" +
                $"&codeVerifier={codeVerifier}" +
                $"&redirectUri={redirectUri}");
            return result.data;
        }

        public async Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto)
        {
            var response = await PostAsync<RequestMovementRequestDto, ApiDataResult<RequestMovementResponseDto>>(
                uri: "/BankOperation/RequestMovements",
                data: requestDto);

            return response.data;
        }

        public async Task<string> CreateAccountConsentsAsync(int firmId)
        {
            var response = await PostAsync<ApiDataResult<string>>(
                uri: $"/OAuth/CreateAccountConsents?firmId={firmId}");
            return response.data;
        }

        public async Task<List<AccountDto>> GetAccountsInConsentAsync(int firmId, string consentId)
        {
            var response = await GetAsync<ApiDataResult<List<AccountDto>>>(
                uri: $"/OAuth/GetAccountsInConsent?firmId={firmId}&consentId={consentId}");

            return response.data;
        }

        public async Task<BaseResponseDto> SaveIntegrationDataAsync(IntegrationDataDto integrationDataDto)
        {
            var result = await PostAsync<IntegrationDataDto, ApiDataResult<BaseResponseDto>>(
                uri: "/OAuth/SaveIntegrationData",
                data: integrationDataDto);

            return result.data;
        }

        public async Task<CompanyDetailsDto> GetCompanyDetailsAsync(int firmId, string accountId)
        {
            var response = await GetAsync<ApiDataResult<CompanyDetailsDto>>(
                uri: $"/OAuth/GetCompanyDetails?firmId={firmId}&accountId={accountId}");

            return response.data;
        }

    }
}
