using Moedelo.Common.Types;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Threading.Tasks;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy
{
    public interface ITradingObjectApiClient
    {
        Task<TradingObjectDto> GetByIdAsync(FirmId firmId, UserId userId, int tradingObjectId);

        Task<TradingObjectShortDto[]> GetShortAsync(FirmId firmId, UserId userId);

        Task<TradingObjectDto[]> GetByCriteriaAsync(FirmId firmId, UserId userId, TradingObjectCriteriaDto criteria);
    }
}
