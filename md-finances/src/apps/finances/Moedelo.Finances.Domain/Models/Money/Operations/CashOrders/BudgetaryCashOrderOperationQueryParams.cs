using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.KbkNumbers;

namespace Moedelo.Finances.Domain.Models.Money.Operations.CashOrders
{
    public class BudgetaryCashOrderOperationQueryParams
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public PaymentDirection? PaymentDirection { get; set; }
        public IReadOnlyCollection<int> BudgetaryTaxesAndFees { get; set; }
        public KbkPaymentType? KbkPaymentType { get; set; }
        public int? KbkId { get; set; }
        public int? BudgetaryYear { get; set; }
        public BudgetaryPeriodType? BudgetaryPeriodType { get; set; }

        public long? PatentId { get; set; }
    }
}
