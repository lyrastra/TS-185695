using System;
using Moedelo.Common.Enums.Enums;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.FinancialOperations;
using BudgetaryPaymentType = Moedelo.Common.Enums.Enums.FinancialOperations.BudgetaryPaymentType;

namespace Moedelo.BizV2.Dto.PaymentOrder
{
    public class DownloadPaymentDto
    {
        public decimal Sum { get; set; }

        public BudgetaryPaymentType BizBudgetaryPaymentType { get; set; }

        public BudgetaryPaymentBase BudgetaryPaymentBase { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public DateTime? Date { get; set; }

        public int PeriodYear { get; set; }

        public int PeriodNumber { get; set; }

        public DocumentFormat DocumentFormat { get; set; }

        public string Kbk { get; set; }

        public BudgetaryPeriodType PeriodType { get; set; }
        
        public int? SettlementAccountId { get; set; }
    }
}