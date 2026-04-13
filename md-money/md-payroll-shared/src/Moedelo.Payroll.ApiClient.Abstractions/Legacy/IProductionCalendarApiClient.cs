using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ProductionCalendars;
using Moedelo.Payroll.Enums.ProductionCalendars;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IProductionCalendarApiClient
    {
        /// <summary>
        /// Возвращает данные по всем дням производственного календаря между указанными датами (включительно)
        /// </summary>
        /// <param name="startDate">начальная дата периода</param>
        /// <param name="endDate">конечная дата периода</param>
        /// <returns>Список дней производственного календаря</returns>
        Task<ProductionCalendarDayDto[]> GetAsync(DateTime startDate, DateTime endDate);

        Task<DateTime> GetWorkingDayByDateAsync(DateTime date);

        Task<DateTime> GetWorkingDayBeforeByDateAsync(DateTime date);

        Task<ProductionCalendarDayType> GetTypeByDateAsync(DateTime date);

        Task<ProductionCalendarDayDto> GetFirstDayOneOfTypesAfterDateAsync(DateTime date, int onNthDay,
            IReadOnlyCollection<ProductionCalendarDayType> dayTypes);

        Task<ProductionCalendarDayDto> GetFirstDayOneOfTypesBeforeDateAsync(DateTime date, int onNthDay,
            IReadOnlyCollection<ProductionCalendarDayType> dayTypes);
    }
}