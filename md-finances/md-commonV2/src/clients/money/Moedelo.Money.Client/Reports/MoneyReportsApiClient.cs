using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Money.Dto.Reports;

namespace Moedelo.Money.Client.Reports
{
    [InjectAsSingleton(typeof(IMoneyReportsApiClient))]
    public class MoneyReportsApiClient : BaseCoreApiClient, IMoneyReportsApiClient
    {
        private readonly ISettingRepository settingRepository;
        private const string prefix = "/private/api/v1";

        public MoneyReportsApiClient(
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
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("MoneyReportsApiEndpoint").Value;
        }

        public Task QueryGetBankAndServiceReportAsync(DownloadGetBankAndServiceReportQueryDto dto)
        {
            var tokenHeaders = GetUnidentifiedTokenHeaders();
            return PostAsync<DownloadGetBankAndServiceReportQueryDto>($"{prefix}/Report/QueryGetBankAndServiceReport", dto);
        }
    }
}
