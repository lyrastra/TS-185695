using Moedelo.Common.Enums.Enums.Calendar;

namespace Moedelo.RptV2.Dto.OutsourceCalendar
{
    public class EventResponseDto
    {
        public int Id { get; set; }

        public int? GroupId { get; set; }

        /// <summary>Название события</summary>
        public string Title { get; set; }

        /// <summary>Краткое название события</summary>
        public string ShortTitle { get; set; }

        /// <summary>Периодичность события</summary>
        public CalendarPeriodType PeriodType { get; set; }
    }
}