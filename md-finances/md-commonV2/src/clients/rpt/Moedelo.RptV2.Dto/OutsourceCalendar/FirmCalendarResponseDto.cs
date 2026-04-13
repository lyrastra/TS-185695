using System;

namespace Moedelo.RptV2.Dto.OutsourceCalendar
{
    public class FirmCalendarResponseDto
    {
        public int Id { get; set; }
        public int FirmId { get; set; }
        public int CalendarId { get; set; }
        public string Title { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsCompleted { get; set; }
        public int? DocumentVersionId { get; set; }
    }
}