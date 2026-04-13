using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.KbkNumbers;
using Moedelo.Common.Enums.Enums.Payroll;
using Moedelo.Common.Enums.Enums.PostingEngine;

namespace Moedelo.Finances.Dto.Money.Operations
{
    public class BudgetaryPaymentForReportDto
    {
        public long Id { get; set; }

        public string PeriodDate { get; set; }

        public decimal Sum { get; set; }

        public string OrderNumber { get; set; }

        public string OrderDate { get; set; }

        public string DocumentTypeDescription { get; set; }

        public string PaymentSnapshot { get; set; }

        public PaysForEmployeeType PaymentType { get; set; }

        public BudgetaryPeriodType? BudgetaryPeriodType { get; set; }

        public long? DocumentBaseId { get; set; }

        public int? KbkId { get; set; }

        public string KbkNumber { get; set; }

        public KbkNumberType? KbkType { get; set; }

        public bool IsPayment { get; set; }

        public int? BudgetaryTaxesAndFees { get; set; }

        public int? BudgetaryPeriodYear { get; set; }

        public int? BudgetaryPeriodNumber { get; set; }

        public long? PatentId { get; set; }

        public int? TradingObjectId { get; set; }

        public OperationType OperationType { get; set; }

        public IReadOnlyCollection<UnifiedBudgetarySubPaymentDto> SubPayments { get; set; }
    }
}
