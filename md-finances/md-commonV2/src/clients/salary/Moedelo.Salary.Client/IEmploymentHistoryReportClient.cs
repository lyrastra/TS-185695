using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Salary.Dto;
using Moedelo.Salary.Dto.EmploymentHistoryReportInfo;

namespace Moedelo.Salary.Client
{
    public interface IEmploymentHistoryReportClient : IDI
    {
        Task<ReportChangedDto> InCompleteByQuickActionAsync(
            int firmId,
            int userId,
            int eventId);

        Task<ReportChangedDto> CompleteByQuickActionAsync(
            int firmId,
            int userId,
            int eventId);

        Task<List<EmptyReportDto>> GetEmptyReportsAsync(int firmId, int userId, int[] firmIds, int[] calendarIds);

        Task<int?> GetFinishedReportWithWorkerAsync(
            int firmId, int userId,
            int workerId, EmploymentChangingType? eventType = null);

        Task DeleteReportByUserEventAsync(int firmId, int userId, int userEventId);
        
        Task<AutoCompleteWizardResponseDto> AutoCompleteWizardAsync(int firmId, int userId,
            AutoCompleteWizardRequestDto request);
    }
}