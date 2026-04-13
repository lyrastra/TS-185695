using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.EarlyPregnancy
{
    public class EarlyPregnancyAllowanceDto
    {
        public DateTime ChargeDate { get; set; }

        public decimal RegionalRate { get; set; }
    }
}
