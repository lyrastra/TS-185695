using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryPaymentReasonDto
    {
        public string Designation { get; set; }

        public string Description { get; set; }

        public BudgetaryPaymentBase Code { get; set; }
    }
}
