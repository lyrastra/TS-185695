using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildBirth
{
    public class ChildBirthAllowanceSaveRequestDto
    {
        public int WorkerId { get; set; }
        public DateTime ChargeDate { get; set; }
        public DateTime ChildBirthDay { get; set; }
        public ChildBirthAllowanceExDto AdditionalData { get; set; }
        public ChildBirthIncomeAndCalculationDto IncomeAndCalculation { get; set; }
        public bool IsSaveRegistry { get; set; }
        public bool CreateOnBehalfOnUser { get; set; }
    }
}