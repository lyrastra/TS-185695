using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryKbkDefaultsRequest
    {
        public int? KbkId { get; set; }

        public BudgetaryAccountCodes AccountCode { get; set; }

        public DateTime? Date { get; set; }

        public int? TradingObjectId { get; set; }

        public BudgetaryPeriod Period { get; set; }
    }
}
