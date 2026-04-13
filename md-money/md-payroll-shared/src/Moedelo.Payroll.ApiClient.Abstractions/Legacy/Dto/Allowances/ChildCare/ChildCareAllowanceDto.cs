using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare
{
    public class ChildCareAllowanceDto
    {
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public DateTime ChildBirthDate { get; set; }
        
        public int Year1 { get; set; }
        
        public int Year2 { get; set; }
        
        public decimal Income1 { get; set; }
        
        public decimal Income2 { get; set; }
        
        public int PeriodDaysCount { get; set; }
        
        public decimal RegionalRate { get; set; }
    }
}