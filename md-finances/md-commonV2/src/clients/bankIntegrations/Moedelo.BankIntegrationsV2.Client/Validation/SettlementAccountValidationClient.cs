using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;

namespace Moedelo.BankIntegrationsV2.Client.Validation
{
    [InjectAsSingleton]
    public class SettlementAccountValidationClient : BaseApiClient, ISettlementAccountValidationClient
    {
        private readonly ISettingRepository settingRepository;

        private const string ControllerName = "/Validation/";

        public SettlementAccountValidationClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("IntegrationApi").Value + ControllerName;
        }
        
        public Task<bool> ValidateNumber(string settlementNumber, IntegrationPartners integrationPartners, int firmId)
        {
            return GetAsync<bool>("/ValidateSettlementAccountNumberAsync", new { settlementNumber, integrationPartners, firmId });
        }
    }
}