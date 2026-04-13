using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Finances.Domain.Models.ClosedPeriods
{
    public class MoneyDocumentsCount
    {
        public AccountingDocumentType Type { get; set; }

        public int Count { get; set; }

        public PaymentDirection Direction { get; set; }

        public int DocumentType { get; set; } = 0;
    }
}