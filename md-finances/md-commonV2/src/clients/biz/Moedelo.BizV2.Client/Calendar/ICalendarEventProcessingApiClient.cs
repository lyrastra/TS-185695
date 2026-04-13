using System.Collections.Generic;
using Moedelo.BizV2.Dto.Calendar;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System.Threading.Tasks;

namespace Moedelo.BizV2.Client.Calendar
{
    public interface ICalendarEventProcessingApiClient : IDI
    {
        /// <summary>
        /// Начать процедуру "by click" переоткрытия календарного события.
        /// Данный метод может заблокировать переоткрытие - в этом случае он должен вернуть соответствующий статус и 
        /// сообщение, которое надо показать пользователю.
        /// Вызывается, когда пользователь пытается переоткрыть событие "быстрым" способом - из виджета календаря и т.п.
        /// </summary>
        /// <param name="firmId"></param>
        /// <param name="userId"></param>
        Task<CalendarEventStatusChangeResponseDto> StartCalendarEventReopenAsync(int firmId, int userId, CalendarEventStatusChangeRequestDto request);
        /// <summary>
        /// Начать процедуру "by click" завершения календарного события.
        /// Данный метод может заблокировать завершение - в этом случае он должен вернуть соответствующий статус и 
        /// сообщение, которое надо показать пользователю.
        /// Вызывается, когда пользователь пытается завершить событие "быстрым" способом - из виджета календаря и т.п.
        /// </summary>
        Task<CalendarEventStatusChangeResponseDto> StartCalendarEventCloseAsync(int firmId, int userId, CalendarEventStatusChangeRequestDto request);
    }
}
