using Moedelo.Common.Enums.Enums.Calendar;

namespace Moedelo.BizV2.Dto.Calendar
{
    public class CalendarEventStatusChangeResponseDto
    {
        /// <summary>
        /// Результат обработки
        /// </summary>
        public СalendarEventActionStatus ResultStatus { get; set; } = СalendarEventActionStatus.Ok;
        /// <summary>
        /// Сообщение, которое надо показать пользователю
        /// </summary>
        public string UserMessage { get; set; } = string.Empty;
    }
}
