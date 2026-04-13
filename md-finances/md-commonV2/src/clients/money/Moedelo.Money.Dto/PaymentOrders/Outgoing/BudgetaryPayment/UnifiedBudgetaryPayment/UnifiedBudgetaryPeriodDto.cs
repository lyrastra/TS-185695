using System;
using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Money.Dto.PaymentOrders.Outgoing.BudgetaryPayment.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryPeriodDto
    {
        public BudgetaryPeriodType Type { get; set; }

        public DateTime? Date { get; set; }

        public int Number { get; set; }

        public int Year { get; set; }
    }
}