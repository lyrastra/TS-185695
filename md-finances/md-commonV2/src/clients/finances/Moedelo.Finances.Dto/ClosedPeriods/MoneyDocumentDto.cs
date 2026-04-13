using System;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Finances.Dto.ClosedPeriods
{
    public class MoneyDocumentDto
    {
        public AccountingDocumentType Type { get; set; }

        public PaymentDirection Direction { get; set; }

        public int DocumentType { get; set; } = 0;

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public long DocumentBaseId { get; set; }
    }
}