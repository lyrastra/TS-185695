using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryPeriodDto
    {
        public BudgetaryPeriodType Type { get; set; }

        public DateTime? Date { get; set; }

        public int Number { get; set; }

        public int Year { get; set; }
    }
}
