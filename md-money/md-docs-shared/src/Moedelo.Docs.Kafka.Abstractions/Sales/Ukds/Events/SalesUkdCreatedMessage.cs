using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Ukds.Events
{
    /// <summary>
    /// Сообщение создания УКД
    /// </summary>
    public class SalesUkdCreatedMessage
    {
        public long DocumentBaseId { get; set; }
        public long SourceDocumentBaseId { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public int KontragentId { get; set; }
        public bool ProvideInAccounting { get; set; }
        public UkdSourceType UkdSourceType { get; set; }
        public long? AdditionalDocumentBaseId { get; set; }
        public long? StockId { get; set; }
        public long? OldRefundDocumentBaseId { get; set; }
        public OperationSource? OldRefundDocumentType { get; set; }
        public IList<SalesUkdItemsCreatedMessage> Items { get; set; }
        public long? RefundDocumentBaseId { get; set; }
        public decimal? RefundSum { get; set; }
        public OperationSource? RefundDocumentType { get; set; }
    }
}
