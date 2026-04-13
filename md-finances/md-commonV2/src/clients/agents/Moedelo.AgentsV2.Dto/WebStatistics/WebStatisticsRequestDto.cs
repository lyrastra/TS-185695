using System;

namespace Moedelo.AgentsV2.Dto.WebStatistics
{
    public class WebStatisticsRequestDto
    {
        public DateTime BeginPeriodDate { get; set; }

        public DateTime EndPeriodDate { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public long? ReferralLinkId { get; set; }

        public bool HasReferralLink { get; set; }

        public int PartnerId { get; set; }
    }
}