using System;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryPaymentAfterAccountingStatementCreatedUpdateRequest
    {
        public long DocumentBaseId { get; set; }

        public long AccountingStatementBaseId { get; set; }

        public DateTime AccountingStatementDate { get; set; }

        public decimal AccountingStatementSum { get; set; }
    }
}
