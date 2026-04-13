using System.Threading.Tasks;
using Moedelo.HistoricalLogsV2.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HistoricalLogsV2.Client
{
    [InjectAsSingleton]
    public class ReportStatusHistoryClient : BaseApiClient, IReportStatusHistoryClient
    {
        private readonly SettingValue endpointSetting;

        public ReportStatusHistoryClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            endpointSetting = settingRepository.Get("HistoricalLogsApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return endpointSetting.Value;
        }

        public Task SaveAsync(ReportStatusHistoryRequestDto requestDto)
        {
            return PostAsync("/ReportStatusHistory/Save", requestDto);
        }
    }
}