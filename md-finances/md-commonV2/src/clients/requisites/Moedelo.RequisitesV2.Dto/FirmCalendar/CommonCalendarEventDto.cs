using System;

namespace Moedelo.RequisitesV2.Dto.FirmCalendar
{
    public class CommonCalendarEventDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Year { get; set; }

        public int PeriodNumber { get; set; }

        public string PeriodType { get; set; }
        
        public string Type { get; set; }
        
        public bool ShowIp { get; set; }
        
        public bool ShowOoo { get; set; }
        
        public string Description { get; set; }
    }
}
