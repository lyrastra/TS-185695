using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Calendar;
using Moedelo.Common.Enums.Enums.Wizard;

namespace Moedelo.RequisitesV2.Dto.FirmCalendar
{
    public class CalendarEventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime LastDateInPeriod { get; set; }
        public CalendarEventStatus Status { get; set; }
        public CalendarEventProtoId ProtoId { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public int PeriodNumber { get; set; }
        public string PeriodType { get; set; }
        public string Url { get; set; }
        public CalendarEventType EventType { get; set; }
        public bool CanSetComplete { get; set; }
        public bool CanSetNotComplete { get; set; }
        public bool IsVisible { get; set; }
        public WizardType WizardType { get; set; }
        public List<CalendarEventTag> Tags { get; set; }
    }
}
