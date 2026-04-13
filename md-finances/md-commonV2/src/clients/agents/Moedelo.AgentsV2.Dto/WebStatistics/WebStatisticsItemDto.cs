namespace Moedelo.AgentsV2.Dto.WebStatistics
{
    public class WebStatisticsItemDto
    {
        public int PartnerId { get; set; }

        public string Date { get; set; }

        public int? CountOfTransitionsByReferalLink { get; set; }

        public int? CountOfLeads { get; set; }

        public int? CountOfConfirmedLeads { get; set; }

        public decimal? ChargedSumForLeads { get; set; }

        public int? CountOfProductSales { get; set; }

        public decimal? ChargedSumForSales { get; set; }

        public decimal? LeadConversion { get; set; }

        public decimal? Profit { get; set; }

        public decimal? TransitionCost { get; set; }

        public long? ReferralLinkId { get; set; }
    }
}