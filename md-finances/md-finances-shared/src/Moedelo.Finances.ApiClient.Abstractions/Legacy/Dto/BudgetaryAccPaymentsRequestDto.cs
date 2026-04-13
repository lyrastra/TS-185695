using System;
using System.Collections.Generic;
using Moedelo.Finances.Enums;

namespace Moedelo.Finances.ApiClient.Abstractions.Legacy.Dto
{
    public class BudgetaryAccPaymentsRequestDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public PaymentDirection? PaymentDirection { get; set; }
        public IReadOnlyCollection<int> BudgetaryTaxesAndFees { get; set; }
        public KbkPaymentType? KbkPaymentType { get; set; }
        public int? KbkId { get; set; }
        public DocumentStatus? PaidStatus { get; set; }
        public int? BudgetaryYear { get; set; }
        public BudgetaryPeriodType? BudgetaryPeriodType { get; set; }
        public int? BudgetaryPeriodNumber { get; set; }
        public bool NeedCashOperations { get; set; }
        public long? PatentId { get; set; }
    }
}