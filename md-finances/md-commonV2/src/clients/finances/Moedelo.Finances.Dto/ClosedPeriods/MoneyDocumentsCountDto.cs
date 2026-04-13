using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Finances.Dto.ClosedPeriods
{
    public class MoneyDocumentsCountDto
    {
        public AccountingDocumentType Type { get; set; }

        public int Count { get; set; }

        public PaymentDirection Direction { get; set; }

        public int DocumentType { get; set; } = 0;

        public List<MoneyDocumentDto> Documents { get; set; }
    }
}