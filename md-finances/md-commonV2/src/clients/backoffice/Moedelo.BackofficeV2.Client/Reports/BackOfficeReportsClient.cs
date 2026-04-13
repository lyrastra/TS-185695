using System.Threading.Tasks;

using Moedelo.BackofficeV2.Dto.Reports;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BackofficeV2.Client.Reports
{
    [InjectAsSingleton]
    public class BackOfficeReportsClient : BaseApiClient, IBackOfficeReportsClient
    {
        private readonly SettingValue apiEndPoint;
        
        public BackOfficeReportsClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("BackOfficePrivateApiEndpoint");
        }

        public Task<bool> SendReportSale2DetailToEmailAsync(ReportSales2ParamDto param)
        {
            return PostAsync<ReportSales2ParamDto, bool>("/SendReportSale2DetailToEmail", param);
        }

        public Task<bool> SendReportLeadChannelsToEmailAsync(ReportLeadChannelsParamDto param)
        {
            return PostAsync<ReportLeadChannelsParamDto, bool>("/SendReportLeadChannels2ToEmail", param);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Rest/Reports/V2";
        }
    }
}
