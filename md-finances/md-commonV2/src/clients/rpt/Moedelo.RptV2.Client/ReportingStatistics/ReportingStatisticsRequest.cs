using Moedelo.Common.Enums.Enums.Reports;

namespace Moedelo.RptV2.Client.ReportingStatistics
{
    public class ReportingStatisticsRequest
    {
        public int QuarterNumber { get; set; }
        public int Year { get; set; }
        public OutsourcerGroup? Group { get; set; }
        public OwnershipForm? OwnershipForm { get; set; }
        public string EmailAddress { get; set; }
    }
}
