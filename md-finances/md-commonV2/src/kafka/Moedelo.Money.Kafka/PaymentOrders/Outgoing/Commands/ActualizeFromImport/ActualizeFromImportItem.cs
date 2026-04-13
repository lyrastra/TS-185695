using System;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.Commands.ActualizeFromImport
{
    public class ActualizeFromImportItem
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
    }
}
