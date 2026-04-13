namespace Moedelo.PayrollV2.Dto.Funds
{
    public class FundChargesSummaryDto
    {
        public int Month { get; set; }

        public decimal TFoms { get; set; }

        public decimal FFoms { get; set; }

        public decimal Accumulate { get; set; }

        public decimal Insurance { get; set; }
    }
}
