using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Ukds.Events
{
    public sealed class SalesUkdDeletedMessage
    {
        public long DocumentBaseId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public UkdSourceType SourceDocumentType { get; set; }

        public long? RefundDocumentBaseId { get; set; }

        public OperationSource? RefundDocumentType { get; set; }

        public List<long> LinkedAccountingStatementsBaseIds { get; set; }

        public int KontragentId { get; set; }
    }
}
