using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.PayrollV2.Client.Reports
{
    [InjectAsSingleton]
    public class ReportsApiV2Client : BaseApiClient, IReportsApiV2Client
    {
        private readonly SettingValue apiEndPoint;

        public ReportsApiV2Client(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Reports";
        }

        public Task<bool> IncompleteReportAsync(int firmId, int userId, int calendarId)
        {
            return GetAsync<bool>("/IncompleteReport", new {firmId, userId, calendarId});
        }

        public Task<bool> CompleteReportAsync(int firmId, int userId, int calendarId)
        {
            return GetAsync<bool>("/CompleteReport", new {firmId, userId, calendarId});
        }
    }
}