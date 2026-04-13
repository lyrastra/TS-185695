using Moedelo.Money.Domain.Operations;

namespace Moedelo.Money.Domain.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetarySubPaymentResponseModel
    {
        public long DocumentBaseId { get; set; }
        public UnifiedBudgetaryKbkResponseModel Kbk { get; set; }
        public BudgetaryPeriod Period { get; set; }
        public decimal Sum { get; set; }
        public long? PatentId { get; set; }
        public bool TaxPostingsInManualMode { get; set; }
    }
}
