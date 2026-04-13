namespace Moedelo.Docs.Kafka.Abstractions.Purchases.CommissionAgentReports.Messages
{
    public class CommissionAgentReportDeletedMessage
    {
        public long DocumentBaseId { get; set; }

        public int KontragentId { get; set; }
    }
}