using Moedelo.Money.Domain.Operations;
using System;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryPaymentKbkDefaultsRequest
    {
        public DateTime? Date { get; set; }

        public int? TradingObjectId { get; set; }

        public BudgetaryPeriod Period { get; set; }
    }
}
