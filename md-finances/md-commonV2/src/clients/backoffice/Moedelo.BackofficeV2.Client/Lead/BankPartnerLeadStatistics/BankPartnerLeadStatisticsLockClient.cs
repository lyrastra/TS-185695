using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BackofficeV2.Client.Lead.BankPartnerLeadStatistics
{
    [InjectAsSingleton(typeof(IBankPartnerLeadStatisticsLockClient))]
    public class BankPartnerLeadStatisticsLockClient : BaseApiClient, IBankPartnerLeadStatisticsLockClient
    {
        private readonly SettingValue backOfficePrivateApiEndpoint;

        public BankPartnerLeadStatisticsLockClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            backOfficePrivateApiEndpoint = settingRepository.Get("BackOfficePrivateApiEndpoint");
        }

        public Task LockBankPartnerLeadStatisticsLockClientAsync()
        {
            const string uri = "/Rest/BankPartnerLeadStatistics/Lock";

            return PostAsync(uri);
        }

        protected override string GetApiEndpoint()
        {
            return backOfficePrivateApiEndpoint.Value;
        }
    }
}
