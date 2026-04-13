using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.AlfabankSso;
using Moedelo.BankIntegrations.ApiClient.Dto.AlfabankSso;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Dto.AlfabankSso;
using Moedelo.BankIntegrations.Dto.InitIntegration;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.BankIntegrations.ApiClient.AlfabankSso
{
    [InjectAsSingleton]
    internal class AlfabankSsoBankAdapterClient : BaseApiClient, IAlfabankSsoBankAdapterClient
    {
        // дефолт в 10 секунд для обращения в банк - маловато
        private static readonly HttpQuerySetting defaultSetting = new HttpQuerySetting(TimeSpan.FromMinutes(1));

        public AlfabankSsoBankAdapterClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<AlfabankSsoBankAdapterClient> logger)
            : base(httpRequestExecuter, uriCreator, auditTracer, authHeadersGetter, auditHeadersGetter, settingRepository.Get("AlfabankSsoAdapterEndpoint"), logger)
        {
        }

        public Task<ApiDataResult<AlfaClientInfoDto>> GetClientInfoAsync(string code, string redirectUri, HttpQuerySetting setting = null)
        {
            var currentSetting = setting ?? defaultSetting;
            var result = GetAsync<ApiDataResult<AlfaClientInfoDto>>(
                uri: "/sso/GetClientInfo",
                queryParams: new { redirectUri, code },
                setting: currentSetting);
            return result;
        }

        public Task<ApiDataResult<InitIntegrationResponseDto>> InitIntegration(InitIntegrationRequestDto request, HttpQuerySetting setting = null)
        {
            var currentSetting = setting ?? defaultSetting;
            var result = PostAsync<InitIntegrationRequestDto, ApiDataResult<InitIntegrationResponseDto>>(
                uri: "/BankOperation/InitIntegration",
                data: request,
                setting: currentSetting);
            return result;
        }

        public Task UpdateIntegrationData(UpdateIntegrationDataDto request, HttpQuerySetting setting = null)
        {
            var currentSetting = setting ?? defaultSetting;
            var result = PostAsync<UpdateIntegrationDataDto, ApiDataResult<BaseResponseDto>>(
                uri: "/Sso/UpdateIntegrationData",
                data: request,
                setting: currentSetting);
            return result;
        }
    }
}
