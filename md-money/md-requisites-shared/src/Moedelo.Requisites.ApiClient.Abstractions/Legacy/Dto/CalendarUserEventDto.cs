using System;
using Moedelo.Requisites.Enums.Calendar;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class CalendarUserEventDto
    {
        public int Id { get; set; }
        public CalendarEventStatus Status { get; set; }
        public int FirmId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public int? Year { get; set; }
        public int? Period { get; set; }
    }
}
