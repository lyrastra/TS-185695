using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryStatusOfPayerDto
    {
        public string Description { get; set; }

        public BudgetaryPayerStatus Code { get; set; }
    }
}
