using System;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Statements.Events
{
    public sealed class SalesStatementDeletedMessage
    {
        public long DocumentBaseId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public int KontragentId { get; set; }
    }
}
