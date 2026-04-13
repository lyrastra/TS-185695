using Moedelo.Requisites.Enums.Calendar;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class CalendarEventUidDto
    {
        public int Id { get; set; }
        public CalendarEventType Type { get; set; }
    }
}