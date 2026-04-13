using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.KbkNumbers;

namespace Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders
{
    public class BudgetaryPaymentOrderOperationQueryParams
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public PaymentDirection? PaymentDirection { get; set; }
        public DocumentStatus? PaidStatus { get; set; }
        public IReadOnlyCollection<int> BudgetaryTaxesAndFees { get; set; }
        public KbkPaymentType? KbkPaymentType { get; set; }
        public KbkNumberType KbkType { get; set; }
        public int? KbkId { get; set; }
        public int? BudgetaryYear { get; set; }
        public BudgetaryPeriodType? BudgetaryPeriodType { get; set; }
        public int? BudgetaryPeriodNumber { get; set; }
        public long? PatentId { get; set; }
    }
}
