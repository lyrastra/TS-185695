using Moedelo.Money.Enums.CashOrders;

namespace Moedelo.Money.Domain.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryKbkResponseModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public UnifiedBudgetaryAccountCodes AccountCode { get; set; }
    }
}
