using System;
using Moedelo.Payroll.Enums.ProductionCalendars;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ProductionCalendars
{
    public class ProductionCalendarDayDto
    {
        public ProductionCalendarDayType DayType { get; set; }
        
        public DateTime Date { get; set; }
    }
}