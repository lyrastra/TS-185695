using System;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.RetailReports.Events
{
    public sealed class SalesRetailReportDeletedMessage
    {
        public long DocumentBaseId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }
    }
}
