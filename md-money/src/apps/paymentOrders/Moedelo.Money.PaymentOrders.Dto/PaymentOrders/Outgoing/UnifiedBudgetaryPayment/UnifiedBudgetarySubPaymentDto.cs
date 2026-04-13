using Moedelo.Money.Enums;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetarySubPaymentDto
    {
        public long DocumentBaseId { get; set; }

        public long ParentDocumentId { get; set; }

        public int KbkId { get; set; }

        public UnifiedBudgetaryPeriodDto Period { get; set; }

        public decimal Sum { get; set; }

        public int? TradingObjectId { get; set; }

        public long? PatentId { get; set; }

        public ProvidePostingType TaxPostingType { get; set; }
    }
}
