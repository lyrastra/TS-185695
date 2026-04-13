using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.BssBanks;
using Moedelo.BankIntegrations.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.BankIntegrations.ApiClient.BssBanks
{
    public class BssBankAdapterClient : BaseApiClient, IBankAdapterClient
    {
        protected readonly ILogger logger;

        public BssBankAdapterClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            SettingValue settingRepository,
            ILogger logger) :
            base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository,
                logger)
        {
            this.logger = logger;
        }

        public virtual async Task<ApiDataResult<T>> GetClientInfoAsync<T>(string redirectUri, string dboServerUri, string authCode, HttpQuerySetting setting = null)
        {
            return await GetAsync<ApiDataResult<T>>("/Sso/GetClientInfo", new { redirectUri, dboServerUri, authCode }, setting: setting);
        }

        public virtual async Task<ApiDataResult<int>> SaveIntegrationDataAsync<T>(T data, HttpQuerySetting setting = null) where T : class
        {
            return await PostAsync<T, ApiDataResult<int>>("/Sso/SaveIntegrationData", data, setting: setting);
        }
    }
}
