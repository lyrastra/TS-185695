using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryKbkRequest
    {
        public string Query { get; set; }

        public BudgetaryAccountCodes AccountCode { get; set; }

        public KbkPaymentType? PaymentType { get; set; }

        public BudgetaryPeriod Period { get; set; }

        public DateTime Date { get; set; }
    }
}
