using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.KbkNumbers;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class NdflPaymentForReportDto
    {
        public string PaymentDate { get; set; }

        public string EndPeriodDate { get; set; }

        public decimal Sum { get; set; }

        public string DocumentNumber { get; set; }

        public AccountingDocumentType DocumentType { get; set; }
        
        public string DocumentTypeDescription { get; set; }

        public BudgetaryPeriodType PeriodType { get; set; }

        public int PeriodNumber { get; set; }

        public KbkNumberType KbkNumberType { get; set; }
        
        public string RecipientOktmo { get; set; }
        
        public string PayerKpp { get; set; }
    }
}