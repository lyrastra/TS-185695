using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Salary.Dto;

namespace Moedelo.Salary.Client
{
    public interface INdfl6ReportClient : IDI
    {
        Task<ReportChangedDto> InCompleteEventByQuickActionAsync(
            int firmId,
            int userId,
            int eventId);

        Task<ReportChangedDto> CompleteEventByQuickActionAsync(
            int firmId,
            int userId,
            int eventId);
    }
}