using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Calendar;

namespace Moedelo.RptV2.Client.ReportStatus
{
    public interface IReportStatusTrackerClient : IDI
    {
        Task WizardOpenedAsync(int firmId, int userId, int calendarId);

        Task WizardCompletedAsync(int firmId, int userId, int calendarId);

        Task EReportCreatedAsync(int firmId, int userId, int calendarId);

        Task EReportSendedAsync(int firmId, int userId, int calendarId);

        Task EReportAccepted(int firmId, int userId, int calendarId, bool isManualReport);

        Task WizardCompletedByArrowAsync(int firmId, int userId, int calendarId, int? eventType);

        Task EReportAcceptedAsync(int firmId, int userId, int calendarId, bool isManualReport);

        Task EReportRejectedAsync(int firmId, int userId, int calendarId, bool isManualReport, string rejectReason);
    }
}
