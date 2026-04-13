using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare
{
    public class ChildCareCalculationRequestDto
    {
        public int WorkerId { get; set; }
        public int YearFirst { get; set; }
        public int YearSecond { get; set; }
        public decimal? YearFirstSum { get; set; }
        public decimal? YearSecondSum { get; set; }
        public bool IsFirstChild { get; set; }
        public int ExcludedDaysCount { get; set; }
        public DateTime VacationStartDate { get; set; }
    }
}