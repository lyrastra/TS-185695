using System;
using Moedelo.Accounting.Enums;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto
{
    public class ReasonDocumentDto
    {
        public int? KontragentId { get; set; }

        public DateTime? DocumentDate { get; set; }

        public string DocumentName { get; set; }

        public DocumentType DocumentType { get; set; }

        public decimal UnpaidBalance { get; set; }

        public TaxationSystemType? DocumentTaxationSystemType { get; set; }

        public long DocumentId { get; set; }

        public long DocumentBaseId { get; set; }

        public decimal Sum { get; set; }
    }
}