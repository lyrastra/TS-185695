using System;
using Moedelo.Payroll.Shared.Enums.Charge;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.Injured
{
    public class EfsInjuredChargeDto
    {
        public long? SettingId { get; set; }

        public int WorkerId { get; set; }

        public decimal Sum { get; set; }

        public decimal SumForFundCharge { get; set; }

        public decimal TaxableSumForNdfl { get; set; }

        public ChargeTypeCode ChargeParentTypeCode { get; set; }

        public ChargeType ChargeType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime ChargeDate { get; set; }

        public bool IsPayKontragent { get; set; }

        public bool IsFuneralAllowance { get; set; }

        public bool IsTaxable { get; set; }

        public bool IsInjuredCharge { get; set; }

        public bool IsAccountablePersonCharge { get; set; }
    }
}