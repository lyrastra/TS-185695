
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class MainKbk
    {
        public int Id { get; set; }

        public BudgetaryAccountCodes AccountCode { get; set; }

        public KbkPaymentType KbkPaymentType { get; set; }
    }
}
