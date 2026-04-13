using System.Collections.Generic;
using Moedelo.Finances.Enums;

namespace Moedelo.Finances.ApiClient.Abstractions.Legacy.Dto
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