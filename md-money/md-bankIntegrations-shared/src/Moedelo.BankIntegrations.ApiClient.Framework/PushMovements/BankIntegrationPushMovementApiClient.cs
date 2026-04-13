using Moedelo.BankIntegrations.ApiClient.Dto.PushMovements;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.PushMovements;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Framework.PushMovements
{
    [InjectAsSingleton(typeof(IBankIntegrationPushMovementApiClient))]
    public class BankIntegrationPushMovementApiClient : BaseCoreApiClient, IBankIntegrationPushMovementApiClient
    {
        private readonly SettingValue endpoint;

        public BankIntegrationPushMovementApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
                : base(httpRequestExecutor, uriCreator, responseParser, settingRepository, auditTracer, auditScopeManager)
        {
            endpoint = settingRepository.Get("IntegrationApiNetCore");
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }

        public Task SendZippedWithoutWsse(PushMovementRequestDto dto)
        {
            return PostAsync("/public/api/v1/PushMovements/SendZippedWithoutWsse", dto);
        }

        public Task SendZippedWithCreateIntegratedRequest(PushMovementRequestDto dto)
        {
            return PostAsync("/public/api/v1/PushMovements/SendZippedWithCreateIntegratedRequest", dto);
        }
    }
}
