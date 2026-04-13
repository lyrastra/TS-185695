namespace Moedelo.Chat.Reports.Dto.SLA
{
    public class SupportSLASummaryDto
    {
        /// <summary>
        /// Count of requests in support for the period
        /// </summary>
        public long SupportRequestsCount { get; set; }

        /// <summary>
        /// Percentage of requests in support, which has time deviation 
        /// </summary>
        public decimal RequestsDeviation { get; set; }

        /// <summary>
        /// Average quality score of requests in support
        /// </summary>
        public decimal QualityScore { get; set; }
    }
}
