namespace Moedelo.Docs.Kafka.Abstractions.Sales.MiddlemanReports.Events
{
    public sealed class SalesMiddlemanReportDeletedMessage
    {
        public long DocumentBaseId { get; set; }
    }
}
