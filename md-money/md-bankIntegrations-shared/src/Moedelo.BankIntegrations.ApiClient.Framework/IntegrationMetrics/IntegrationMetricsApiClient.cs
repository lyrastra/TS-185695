using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMetrics;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationMetrics;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrations.ApiClient.Framework.IntegrationMetrics
{
    [InjectAsSingleton(typeof(IIntegrationMetricsApiClient))]
    public class IntegrationMetricsApiClient : BaseCoreApiClient, IIntegrationMetricsApiClient
    {
        private readonly SettingValue endpoint;
        private const string ControllerName = "/private/api/v1/IntegrationMetrics";

        public IntegrationMetricsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.endpoint = settingRepository.Get("IntegrationApiNetCore");
        }
        
        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }
        
        public async Task CreateTurnOnIntegrationMetricAsync(TurnOnIntegrationMetricDto dto)
        {
            var tokenHeader = await GetPrivateTokenHeaders(dto.FirmId, dto.UserId).ConfigureAwait(false);
            await PostAsync($"{ControllerName}/Create", dto, queryHeaders: tokenHeader).ConfigureAwait(false);
        }
    }
}