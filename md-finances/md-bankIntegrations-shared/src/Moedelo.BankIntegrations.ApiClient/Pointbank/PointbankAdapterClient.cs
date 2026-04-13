using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.Pointbank;
using Moedelo.BankIntegrations.ApiClient.Dto.Accounts;
using Moedelo.BankIntegrations.ApiClient.Dto.PointBank.Consents;
using Moedelo.BankIntegrations.ApiClient.Dto.PointBank.Customers;
using Moedelo.BankIntegrations.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.BankIntegrations.Dto.Movements;

namespace Moedelo.BankIntegrations.ApiClient.Pointbank
{
    [InjectAsSingleton(typeof(IPointbankAdapterClient))]
    public class PointbankAdapterClient : BaseApiClient, IPointbankAdapterClient
    {
        private readonly string ssoController = "Sso";
        
        public PointbankAdapterClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PointbankAdapterClient> logger) : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("PointbankAdapterEndpoint"),
                logger)
        {

        }
        
        public async Task<bool> WriteTokenByCodeAsync(int firmId, string code, string redirectUri)
        {
            var result = await GetAsync<ApiDataResult<bool>>(uri: "/OAuth/WriteTokenByCode", queryParams: new { firmId, code, redirectUri });
            return result.data;
        }

        public async Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto)
        {
            var response = await PostAsync<RequestMovementRequestDto, ApiDataResult<RequestMovementResponseDto>>(
                uri: "/BankOperation/RequestMovements",
            data: requestDto);

            return response.data;
        }
        public async Task<string> CreateConsentIdAsync(string[] permissions)
        {
            var result = await PostAsync<string[], ApiDataResult<string>>(uri: $"/{ssoController}/CreateConsent", data: permissions);
            return result.data;
        }

        public async Task<ConsentDto> GetConsentInfoAsync(int firmId, string consentId)
        {
            var result = await GetAsync<ApiDataResult<ConsentDto>>(uri: $"/{ssoController}/GetConsentInfo", queryParams: new { firmId, consentId });
            return result.data;
        }

        public async Task<GetConsentsDto> GetAllChildConsentsAsync(int firmId, string consentId)
        {
            var result = await GetAsync<ApiDataResult<GetConsentsDto>>(uri: $"/{ssoController}/GetAllChildConsents", queryParams: new { firmId, consentId });
            return result.data;
        }
        
        public async Task<List<CustomerDto>> GetCustomersListAsync(int firmId)
        {
            var response = await GetAsync<ApiDataResult<List<CustomerDto>>>(
                uri: $"/{ssoController}/GetCustomersList",
                queryParams: new { firmId },
                setting: new HttpQuerySetting(TimeSpan.FromMinutes(1)));
            return response.data;
        }
        
        public async Task<GetAccountsResponseDto> GetAccountsByCustomerCodeAsync(int firmId, string customerCode)
        {
            var response = await GetAsync<ApiDataResult<GetAccountsResponseDto>>(
                uri: "/BankOperation/GetAccountsByCustomerCode",
                queryParams: new { firmId, customerCode },
                setting: new HttpQuerySetting(TimeSpan.FromMinutes(1)));
            return response.data;
        }
    }
}
