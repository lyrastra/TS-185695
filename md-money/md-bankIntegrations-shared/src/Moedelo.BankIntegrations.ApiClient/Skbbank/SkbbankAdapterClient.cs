using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.Skbbank;
using Moedelo.BankIntegrations.ApiClient.Dto.Skbbank;
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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Skbbank
{
    [InjectAsSingleton]
    public class SkbbankAdapterClient : BaseApiClient, ISkbbankAdapterClient
    {
        public SkbbankAdapterClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SkbbankAdapterClient> logger) : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("SkbbankAdapterEndpoint"),
                logger)
        {
        }

        public Task<UserInfoDto> GetUserInfo(string code)
        {
            return GetAsync<UserInfoDto>(
             uri: "/sso/GetUserInfo",
             queryParams: new { code },
             setting: new HttpQuerySetting(TimeSpan.FromMinutes(1)));
        }

        public Task<SkbMovementRequestResponseDto> InitRequestMovement(string accountNumber, int firmId)
        {
            return GetAsync<SkbMovementRequestResponseDto>(
                uri: "/sso/InitRequestMovement",
                queryParams: new {accountNumber, firmId},
                setting: new HttpQuerySetting(TimeSpan.FromMinutes(1)));
        }

        public Task<string> GetServId(int firmId)
        {
            return GetAsync<string>(
               uri: "/sso/GetServId",
               queryParams: new { firmId },
               setting: new HttpQuerySetting(TimeSpan.FromMinutes(1)));
        }
        
        public Task<Dictionary<int, string>> TransferToken()
        {
            return PostAsync<Dictionary<int, string>>("/Technical/TransferTokenForSkb");
        }
    }
}
