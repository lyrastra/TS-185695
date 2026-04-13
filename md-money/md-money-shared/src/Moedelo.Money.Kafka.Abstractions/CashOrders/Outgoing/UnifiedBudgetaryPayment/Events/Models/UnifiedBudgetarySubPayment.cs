using Moedelo.Money.Kafka.Abstractions.Models;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment.Events.Models
{
    public class UnifiedBudgetarySubPayment
    {
        public long DocumentBaseId { get; set; }

        public decimal Sum { get; set; }

        public int KbkId { get; set; }

        public BudgetaryPeriod Period { get; set; }

        public long? PatentId { get; set; }

        public bool IsManualTaxPostings { get; set; }
    }
}
