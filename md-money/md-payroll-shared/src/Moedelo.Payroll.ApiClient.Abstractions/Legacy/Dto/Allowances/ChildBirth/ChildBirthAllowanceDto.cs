using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildBirth
{
    public class ChildBirthAllowanceDto
    {
        public DateTime BirthDate { get; set; }

        public DateTime ChargeDate { get; set; }
        
        public decimal RegionalRate { get; set; }
    }
}