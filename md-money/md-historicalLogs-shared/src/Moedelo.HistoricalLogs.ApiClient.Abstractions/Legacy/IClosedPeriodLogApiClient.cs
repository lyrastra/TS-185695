using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.HistoricalLogs.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.HistoricalLogs.Enums;

namespace Moedelo.HistoricalLogs.ApiClient.Abstractions.Legacy
{
    public interface IClosedPeriodLogApiClient
    {
        Task<ClosedPeriodPageResponseDto> GetByCriteriaAsync(ClosedPeriodLogGetDto dto);
    }
}
