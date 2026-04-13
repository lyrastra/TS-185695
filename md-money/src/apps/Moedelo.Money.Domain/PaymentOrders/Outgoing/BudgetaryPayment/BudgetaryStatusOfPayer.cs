using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryStatusOfPayer
    {
        public string Description { get; set; }

        public BudgetaryPayerStatus Code { get; set; }
    }
}
