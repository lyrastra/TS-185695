using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare
{
    public class ChildCareAllowanceSaveRequestDto
    {
        public int WorkerId { get; set; }
        public bool IsFirstChild { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime? CompensationStartDate { get; set; }
        public bool IsParent { get; set; }
        public ChildCareAllowanceExDto AdditionalData { get; set; }
        public ChildCareIncomeAndCalculationDto IncomeAndCalculation { get; set; }
        public bool IsSaveRegistry { get; set; }
        public bool CreateOnBehalfOnUser { get; set; }
    }
}