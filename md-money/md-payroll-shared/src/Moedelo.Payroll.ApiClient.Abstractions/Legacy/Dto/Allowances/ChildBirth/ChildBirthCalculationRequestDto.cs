using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildBirth
{
    public class ChildBirthCalculationRequestDto
    {
        public int WorkerId { get; set; }
        public DateTime ChargeDate { get; set; }
        public DateTime ChildBirthDay { get; set; }
    }
}