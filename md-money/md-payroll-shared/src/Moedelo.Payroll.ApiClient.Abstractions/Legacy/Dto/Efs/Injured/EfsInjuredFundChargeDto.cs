using System;
using Moedelo.Payroll.Enums.Funds;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.Injured
{
    public class EfsInjuredFundChargeDto
    {
        public int WorkerId { get; set; }

        public DateTime PostingDate { get; set; }

        public FundChargeType FundChargeType { get; set; }

        public bool IsMrotOverdraft { get; set; }

        public bool IsBaseOverdraft { get; set; }

        public decimal Sum { get; set; }

        public decimal BaseChargeSum { get; set; }

        public bool IsGpd { get; set; }
    }
}