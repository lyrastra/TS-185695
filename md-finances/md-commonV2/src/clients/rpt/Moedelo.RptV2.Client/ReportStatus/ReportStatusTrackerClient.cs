using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.RptV2.Client.ReportStatus
{
    [InjectAsSingleton]
    public class ReportStatusTrackerClient : BaseApiClient, IReportStatusTrackerClient
    {
        private readonly SettingValue apiEndpoint;
        public ReportStatusTrackerClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("RptApiEndpoint");
        }
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task EReportCreatedAsync(int firmId, int userId, int calendarId)
        {
            return GetAsync("/ReportStatusTracker/EReportCreated", new { firmId, userId, calendarId });
        }

        public Task EReportSendedAsync(int firmId, int userId, int calendarId)
        {
            return GetAsync("/ReportStatusTracker/EReportSended", new { firmId, userId, calendarId });
        }

        public Task EReportAccepted(int firmId, int userId, int calendarId, bool isManualReport)
        {
            return GetAsync("/ReportStatusTracker/EReportAccepted", new { firmId, userId, calendarId, isManualReport });
        }

        public Task WizardCompletedAsync(int firmId, int userId, int calendarId)
        {
            return GetAsync("/ReportStatusTracker/WizardCompleted", new { firmId, userId, calendarId });
        }

        public Task WizardOpenedAsync(int firmId, int userId, int calendarId)
        {
            return GetAsync("/ReportStatusTracker/WizardOpened", new { firmId, userId, calendarId});
        }

        public Task WizardCompletedByArrowAsync(int firmId, int userId, int calendarId, int? eventType)
        {
            return GetAsync("/ReportStatusTracker/WizardCompletedByArrow", new { firmId, userId, calendarId, eventType });
        }

        public Task EReportAcceptedAsync(int firmId, int userId, int calendarId, bool isManualReport)
        {
            return GetAsync("/ReportStatusTracker/WizardOpened", new { firmId, userId, calendarId, isManualReport });
        }

        public Task EReportRejectedAsync(int firmId, int userId, int calendarId, bool isManualReport, string rejectReason)
        {
            var request = new
            {
                CalendarId = calendarId,
                IsManualReport = isManualReport,
                RejectReason = rejectReason
            };

            return PostAsync($"/ReportStatusTracker/EReportRejected?firmId={firmId}&userId={userId}", request);
        }
    }
}
