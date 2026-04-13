using System;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.KbkNumbers;

namespace Moedelo.AccountingV2.Dto.Payments.Requests
{
    public class GetFundAccPaymentsRequestDto
    {
        public DateTime? StartDate { get; set; } 
        
        public DateTime? EndDate { get; set; }
        
        public  PaymentDirection? PaymentDirection { get; set; }
        
        public int? BudgetaryTaxesAndFees { get; set; }
        
        public KbkPaymentType? KbkPaymentType { get; set; }
        
        public int? KbkId { get; set; }
        
        public DocumentStatus? PaidStatus { get; set; }
        
        public int? BudgetaryYear { get; set; }
        
        public BudgetaryPeriodType? BudgetaryPeriodType { get; set; }
    }
}