namespace Moedelo.Docs.Kafka.Abstractions.Sales.RetailReports.Events
{
    public class SalesRetailReportItem
    {
        public long Id { get; set; }

        public long StockProductId { get; set; }

        public string Name { get; set; }

        public string Unit { get; set; }

        public decimal Count { get; set; }

        public decimal SaleSum { get; set; }
    }
}
