using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryPeriodDto
    {
        public BudgetaryPeriodType Type { get; set; }

        public DateTime? Date { get; set; }

        public int Number { get; set; }

        public int Year { get; set; }
    }
}
