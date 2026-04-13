using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.KbkNumbers;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class NdflRefundFromBudgetPaymentForReportDto
    {
        public string PaymentDate { get; set; }

        public decimal Sum { get; set; }

        public string DocumentNumber { get; set; }

        public AccountingDocumentType DocumentType { get; set; }

        public string DocumentTypeDescription { get; set; }

        public KbkNumberType KbkNumberType { get; set; }
    }
}