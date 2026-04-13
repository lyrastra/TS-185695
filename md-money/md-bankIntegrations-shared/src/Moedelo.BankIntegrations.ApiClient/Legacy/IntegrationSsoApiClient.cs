using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.Legacy;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IIntegrationSsoApiClient))]
    public class IntegrationSsoApiClient : BaseApiClient, IIntegrationSsoApiClient
    {
        private const string ControllerName = "IntegratedUser";
        public IntegrationSsoApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<IntegratedUserApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("IntegrationApi"),
                logger)
        {
        }

        public async Task SaveFromSso(SaveFromSsoDto request, HttpQuerySetting setting = null)
        {
            await PostAsync($"/{ControllerName}/SaveFromSso", request, setting: setting);
        }
    }
}
