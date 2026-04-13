using Moedelo.HistoricalLogs.ApiClient.Abstractions.Legacy.Dto;
using System.Threading.Tasks;

namespace Moedelo.HistoricalLogs.ApiClient.Abstractions.Legacy
{
    public interface IOperationLogApiClient
    {
        Task LogAsync(LogOperationDto data);
    }
}
