namespace Moedelo.OfficeV2.Dto.Finance
{
    public class FinanceReportDto
    {
        public int Year { get; set; }

        public FinanceIncomingDto Incoming { get; set; }

        public FinanceOutgoingDto Outgoing { get; set; }

        public FinanceProfitabilityDto Profitability { get; set; }

        public FinanceSolvencyDto Solvency { get; set; }

        public FinancialStabilityDto Stability { get; set; }

        public FinanceMonetaryFlowsDto MonetaryFlows { get; set; }
    }
}
