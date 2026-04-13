using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Domain.TaxPostings;

namespace Moedelo.Money.Domain.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetarySubPaymentSaveModel
    {
        public long DocumentBaseId { get; set; }
        public int KbkId { get; set; }
        public BudgetaryPeriod Period { get; set; }
        public decimal Sum { get; set; }
        public long? PatentId { get; set; }
        public TaxPostingsData TaxPostings { get; set; }
    }
}
