using Moedelo.Common.Enums.Enums.FinancialOperations;
using System;

namespace Moedelo.RptV2.Dto.UsnDeclaration
{
    public class BankPaymentDto
    {
        public decimal Sum { get; set; }

        public BudgetaryPaymentType BizBudgetaryPaymentType { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public DateTime? Date { get; set; }

        public int PeriodYear { get; set; }

        public int PeriodNumber { get; set; }

        public string Kbk { get; set; }

        public BudgetaryPaymentBase BudgetaryPaymentBase { get; set; }
        
        public int? SettlementAccountId { get; set; }
    }
}
