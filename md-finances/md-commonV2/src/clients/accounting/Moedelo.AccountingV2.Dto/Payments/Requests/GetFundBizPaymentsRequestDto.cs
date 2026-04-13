using System;
using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.AccountingV2.Dto.Payments.Requests
{
    public class GetFundBizPaymentsRequestDto
    {
        public DateTime? StartDate { get; set; } 
        
        public DateTime? EndDate { get; set; }
        
        public string Type { get; set; }
        
        public BudgetaryPaymentType? BudgetadyPaymentType { get; set; }
        
        public BudgetaryPaymentSubtype? BudgetadyPaymentSubtype { get; set; }
        
        public int? YearPeriod { get; set; }
    }
}