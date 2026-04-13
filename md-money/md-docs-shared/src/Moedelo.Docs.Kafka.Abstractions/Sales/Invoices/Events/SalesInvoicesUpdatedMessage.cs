using System.Collections.Generic;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Invoices.Events
{
    public class SalesInvoicesUpdatedMessage
    {
        public List<SalesInvoiceMessage> Invoices { get; set; }
    }
}