using Moedelo.Money.Enums;

namespace Moedelo.Money.CashOrders.Dto.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetarySubPaymentDto
    {
        public long DocumentBaseId { get; set; }

        public int KbkId { get; set; }

        public UnifiedBudgetaryPeriodDto Period { get; set; }

        public decimal Sum { get; set; }

        public long? PatentId { get; set; }

        public ProvidePostingType TaxPostingType { get; set; }
    }
}
