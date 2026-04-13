using System.Threading.Tasks;
using Moedelo.HistoricalLogs.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.HistoricalLogs.ApiClient.Abstractions.Legacy
{
    public interface IOutsourceCalendarLogApiClient
    {
        Task LogAsync(OutsourceCalendarLogDto data);
    }
}