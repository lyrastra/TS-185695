using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.IntegrationRequest;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrationsV2.Client.IntegrationRequest
{
    [InjectAsSingleton]
    public class IntegrationRequestClient : BaseApiClient, IIntegrationRequestClient
    {
        private const string ControllerName = "/IntegrationRequest/";
        private readonly SettingValue apiEndPoint;

        public IntegrationRequestClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator,
            IResponseParser responseParser, 
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager, 
            ISettingRepository settingRepository) 
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IntegrationApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }

        public Task ResetCreatedRequestsToErrorStatus(IntegrationRequestResetDto dto)
        {
            return GetAsync<IntegrationRequestResetDto>("ResetCreatedRequestsToErrorStatus", dto);
        }
    }
}