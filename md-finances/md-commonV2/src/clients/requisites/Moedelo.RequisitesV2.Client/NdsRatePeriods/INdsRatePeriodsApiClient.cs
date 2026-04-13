using System.Threading;
using System.Threading.Tasks;
using Moedelo.RequisitesV2.Dto.NdsRatePeriods;

namespace Moedelo.RequisitesV2.Client.NdsRatePeriods
{
    /// <summary>
    /// Работа с настройками "Применяемая ставка НДС"
    /// </summary>
    public interface INdsRatePeriodsApiClient
    {
        /// <summary> Возвращает настройки </summary>
        Task<NdsRatePeriodDto[]> GetAsync(int firmId, int userId, CancellationToken ct = default);
    }
}
