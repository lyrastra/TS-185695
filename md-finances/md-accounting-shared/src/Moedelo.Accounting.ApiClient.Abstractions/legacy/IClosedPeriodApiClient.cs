using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using System;
using System.Threading.Tasks;
using Moedelo.Accounting.Enums.ClosedPeriods;
using Moedelo.Common.Types;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.ClosedPeriods;
using System.Collections.Generic;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    /// <summary>
    /// Клиент для работы с периодами МЗМ
    /// </summary>
    public interface IClosedPeriodApiClient
    {
        /// <summary>
        /// Последняя дата в закрытом периоде (если такого нет: дата ввода остатков/дата гос. регистрации)
        /// </summary>
        Task<DateTime> GetLastClosedDateAsync(FirmId firmId, UserId userId);

        /// <summary>
        /// Первая дата в открытом периоде
        /// </summary>
        Task<DateTime> GetFirstOpenedDateAsync(FirmId firmId, UserId userId);

        /// <summary>
        /// Последний закрытый период
        /// </summary>
        Task<ClosedPeriodDto> GetLastClosedPeriodAsync(FirmId firmId, UserId userId);

        /// <summary>
        /// Дата окончания последнего закрытого периода по списку фирм
        /// </summary>
        Task<IReadOnlyDictionary<int, DateTime>> GetLastClosedDateByFirmIdsAsync(
            IReadOnlyCollection<int> firmIds);

        /// <summary>
        /// Проверяет на ошибки и предупреждения (аналогично 1 шагу мастера МЗМ в ЛК)
        /// </summary>
        Task<CheckPerionValidationDto> CheckPeriodAsync(FirmId firmId, UserId userId, DateTime startDate, DateTime endDate, int timeoutMs = 30_000);

        /// <summary>
        /// Закрытие месяца
        /// </summary>
        Task CloseMonthAsync(FirmId firmId, UserId userId, DateTime upToDate, CloseType closeType = CloseType.Default, int timeoutMs = 30_000);
        
        /// <summary>
        /// Закрытие периода по календарному событию (не более 1 квартала)
        /// </summary>
        Task ClosePeriodByCalendarIdAsync(FirmId firmId, UserId userId, int calendarId, CloseType closeType = CloseType.Default, int timeoutMs = 30_000);
    }
}
