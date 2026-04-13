using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Commands.ActualizeFromImport
{
    public class ActualizeFromImportItem
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
        public bool IsOutsourceApproved { get; set; }
    }
}
