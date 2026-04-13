using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.PaymentDocuments
{
    public class PaymentDocumentLinkRequestDto
    {
        public int FirmId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public int KontragentId { get; set; }

        public AccountingDocumentType DocumentType { get; set; }
    }
}
