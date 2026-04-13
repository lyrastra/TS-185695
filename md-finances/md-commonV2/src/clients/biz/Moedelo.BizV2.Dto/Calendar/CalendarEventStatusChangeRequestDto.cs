using Moedelo.Common.Enums.Enums.Calendar;

namespace Moedelo.BizV2.Dto.Calendar
{
    public class CalendarEventStatusChangeRequestDto
    {
        public int Id { get; set; }
        public CalendarEventProtoId ProtoId { get; set; }
        public bool ConfirmedByUser { get; set; }
    }
}
