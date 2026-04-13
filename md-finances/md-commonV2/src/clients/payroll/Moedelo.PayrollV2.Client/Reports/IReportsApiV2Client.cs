using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.PayrollV2.Client.Reports
{
    public interface IReportsApiV2Client : IDI
    {
        // Вернуть в актуальные ЗП мастер по календарному событию 
        Task<bool> IncompleteReportAsync(int firmId, int userId, int calendarId);

        // Завершить ЗП мастер по календарному событию
        Task<bool> CompleteReportAsync(int firmId, int userId, int calendarId);
    }
}