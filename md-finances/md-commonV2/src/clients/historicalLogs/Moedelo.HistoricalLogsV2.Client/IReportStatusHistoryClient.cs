using System.Threading.Tasks;
using Moedelo.HistoricalLogsV2.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.HistoricalLogsV2.Client
{
    public interface IReportStatusHistoryClient : IDI
    {
        Task SaveAsync(ReportStatusHistoryRequestDto requestDto);
    }
}