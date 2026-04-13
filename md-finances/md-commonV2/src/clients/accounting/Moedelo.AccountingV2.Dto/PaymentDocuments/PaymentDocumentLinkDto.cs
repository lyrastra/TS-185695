using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.PaymentDocuments
{
    public class PaymentDocumentLinkDto
    {
        public int EntityId { get; set; }

        public long LinkedDocumentId { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public decimal OperationSum { get; set; }

        public DateTime Date { get; set; }

        public AccountingDocumentType Type { get; set; }

        public bool IsCash { get; set; }
    }
}
