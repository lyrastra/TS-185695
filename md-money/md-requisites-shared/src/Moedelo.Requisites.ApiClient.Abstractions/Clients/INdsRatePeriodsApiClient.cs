using System.Threading;
using System.Threading.Tasks;
using Moedelo.Requisites.ApiClient.Abstractions.Clients.Dto;

namespace Moedelo.Requisites.ApiClient.Abstractions.Clients;

/// <summary>
/// Работа с настройками "Применяемая ставка НДС"
/// </summary>
public interface INdsRatePeriodsApiClient
{
    /// <summary> Возвращает настройки </summary>
    Task<NdsRatePeriodDto[]> GetAsync(GetNdsRatePeriodsFilterDto dto, CancellationToken ct = default);

    /// <summary> Возвращает ставки НДС по переданным индентификаторам </summary>
    Task<NdsRatePeriodDto[]> GetAsync(int userId, int firmId, CancellationToken ct = default);
}