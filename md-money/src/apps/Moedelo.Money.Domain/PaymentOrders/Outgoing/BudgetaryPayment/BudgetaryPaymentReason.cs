using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryPaymentReason
    {
        public string Designation { get; set; }

        public string Description { get; set; }

        public BudgetaryPaymentBase Code { get; set; }
    }
}
