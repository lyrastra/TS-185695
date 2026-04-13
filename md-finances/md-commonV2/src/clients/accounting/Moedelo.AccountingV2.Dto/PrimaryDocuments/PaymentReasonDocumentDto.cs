using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using System;

namespace Moedelo.AccountingV2.Dto.PrimaryDocuments
{
    public class PaymentReasonDocumentDto
    {
        public DateTime? DocumentDate { get; set; }

        public string DocumentName { get; set; }

        public AccountingDocumentType DocumentType { get; set; }

        public decimal UnpaidBalance { get; set; }

        public TaxationSystemType? DocumentTaxationSystemType { get; set; }

        public long DocumentId { get; set; }

        public long DocumentBaseId { get; set; }

        public decimal Sum { get; set; }

        public int? KontragentId { get; set; }
    }
}
