using System;
using Moedelo.Payroll.Enums.Allowances;

namespace  Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances
{
    public class AllowanceListItemDto
    {
        public int WorkerId { get; set; }

        public long AllowanceId { get; set; }

        public string Period { get; set; }

        public DateTime StartPeriod { get; set; }

        public DateTime? EndPeriod { get; set; }

        public ChargeType AllowanceType { get; set; }
    }
}