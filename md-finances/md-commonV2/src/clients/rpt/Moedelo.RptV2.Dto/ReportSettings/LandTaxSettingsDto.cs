using Moedelo.Common.Enums.Enums.Reports;

namespace Moedelo.RptV2.Dto.ReportSettings
{
    public class LandTaxSettingsDto
    {
        public OrderOfPayment OrderOfPayment { get; set; }

        public int AdvancesDay { get; set; }
        public int AdvancesMonth { get; set; }

        public int AnnualDay { get; set; }
        public int AnnualMonth { get; set; }

        public bool HasQuarterlyEvents { get; set; }
    }
}