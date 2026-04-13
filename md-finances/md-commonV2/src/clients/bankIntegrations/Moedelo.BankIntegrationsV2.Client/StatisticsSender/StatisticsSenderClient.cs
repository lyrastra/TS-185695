using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrationsV2.Client.StatisticsSender
{
    [InjectAsSingleton]
    public class StatisticsSenderClient : BaseApiClient, IStatisticsSenderClient
    {
        private const string ControllerName = "/StatisticsSender/";
        private readonly SettingValue apiEndPoint;

        public StatisticsSenderClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IntegrationApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }

        public Task SendForSberbankAsync()
        {
            return PostAsync("SendForSberbank");
        }

        public Task CheckSberbankUpgAsync()
        {
            return PostAsync("CheckSberbankUpg");
        }

        public Task SendEmailAboutExpiriesCertificatesAsync()
        {
            return PostAsync("SendEmailAboutExpiriesCertificates");
        }

        public Task SendEmailAboutMorningInfoAsync()
        {
            return PostAsync("SendEmailAboutMorningInfo");
        }
    }
}