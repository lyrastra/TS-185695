using Moedelo.ReceiptStatement.Kafka.Abstractions.Models;
using System;
using System.Collections.Generic;

namespace Moedelo.ReceiptStatement.Kafka.Abstractions.Messages
{
    public class ReceiptStatementCreatedMessage
    {
        public long DocumentBaseId { get; set; }

        public long SubcontoId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public decimal NdsSum { get; set; }

        public IReadOnlyCollection<PaymentLink> PaymentLinks { get; set; }

        public long FixedAssetDocumentBaseId { get; set; }

        public int KontragentId { get; set; }

        public int ContractId { get; set; }

        public long ContractDocumentBaseId { get; set; }
    }
}
