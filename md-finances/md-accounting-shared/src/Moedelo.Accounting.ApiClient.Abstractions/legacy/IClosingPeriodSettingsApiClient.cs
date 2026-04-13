using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Types;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    /// <summary>
    /// Клиент настроек МЗМ
    /// </summary>
    public interface IClosingPeriodSettingsApiClient
    {
        /// <summary>
        /// Возвращает настройки МЗМ для фирмы
        /// </summary>
        Task<ClosingPeriodSettingsDto> GetAsync(FirmId firmId, UserId userId);

        /// <summary>
        /// Возвращает дату начала действия МЗМ для списка фирм
        /// </summary>
        Task<IReadOnlyDictionary<int, int?>> GetByFirmIdsAsync(
            IReadOnlyCollection<int> firmIds);
    }
}