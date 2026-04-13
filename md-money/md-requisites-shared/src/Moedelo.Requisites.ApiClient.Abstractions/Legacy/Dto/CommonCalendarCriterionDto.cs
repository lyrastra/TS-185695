using Moedelo.Requisites.Enums.Calendar;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class CommonCalendarCriterionDto
    {
        public CalendarEventProtoId? ProtoId { get; set; }

        public int? Year { get; set; }

        public int? PeriodNumber { get; set; }
    }
}