using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.HistoricalLogsV2.Dto.ClosedPeriodLog;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.HistoricalLogsV2.Client.ClosedPeriodLog
{
    public interface IClosedPeriodLogApiClient : IDI
    {
        Task LogAsync(ClosedPeriodLogRequestDto dto);

        Task<IReadOnlyList<ClosedPeriodLogResponseDto>> ByFirmIdAsync(int firmId);
    }
}
