using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class DeductionPaymentDto
    {
        public int? WorkerId { get; set; }

        public int KontragentId { get; set; }

        public decimal Sum { get; set; }

        public DateTime Date { get; set; }

        public string DocumentNumber { get; set; }

        public AccountingDocumentType DocumentType { get; set; }

        public long DocumentBaseId { get; set; }
    }
}