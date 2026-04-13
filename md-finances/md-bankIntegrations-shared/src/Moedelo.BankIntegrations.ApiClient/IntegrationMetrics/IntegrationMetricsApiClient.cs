using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.IntegrationMetrics;
using Moedelo.BankIntegrations.ApiClient.BankIntegrationsNetCore;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMetrics;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.ApiClient.IntegrationMetrics
{
    [InjectAsSingleton(typeof(IIntegrationMetricsApiClient))]
    public class IntegrationMetricsApiClient : BaseApiClient, IIntegrationMetricsApiClient
    {
        private const string ControllerName = "/private/api/v1/IntegrationMetrics";
        
        public IntegrationMetricsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<BankOperationClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("IntegrationApiNetCore"),
                logger)
        {
        }
        
        public async Task CreateTurnOnIntegrationMetricAsync(TurnOnIntegrationMetricDto dto)
        {
            await PostAsync($"{ControllerName}/Create", dto);
        }
    }
}