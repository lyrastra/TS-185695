using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.CashOrders.Dto.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryPeriodDto
    {
        public BudgetaryPeriodType Type { get; set; }

        public DateTime? Date { get; set; }

        public int Number { get; set; }

        public int Year { get; set; }
    }
}
