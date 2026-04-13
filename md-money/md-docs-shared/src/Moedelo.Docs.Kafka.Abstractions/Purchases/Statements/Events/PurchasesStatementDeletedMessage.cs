using System;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.Statements.Events
{
    public class PurchasesStatementDeletedMessage
    {
        public long DocumentBaseId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public int KontragentId { get; set; }
    }
}
