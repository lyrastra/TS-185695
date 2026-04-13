using Moedelo.Money.Enums;

namespace Moedelo.Money.PaymentOrders.Domain.Models.BudgetaryPayment
{
    public class BudgetaryPaymentReason
    {
        public long Id { get; set; }

        public string Designation { get; set; }

        public string Description { get; set; }

        public BudgetaryPaymentBase Code { get; set; }
    }
}
