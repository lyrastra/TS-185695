using Moedelo.Money.Kafka.Abstractions.Models;
using System.Collections.Generic;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Models
{
    public class UnifiedBudgetarySubPayment
    {
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Идентификатор КБК
        /// </summary>
        public int KbkId { get; set; }

        /// <summary>
        /// Бюджетный период
        /// </summary>
        public BudgetaryPeriod Period { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Идентификатор торгового объекта
        /// </summary>
        public int? TradingObjectId { get; set; }

        /// <summary>
        /// Идентификатор патента
        /// </summary>
        public long? PatentId { get; set; }

        public IReadOnlyCollection<DocumentLink> CurrencyInvoicesLinks { get; set; }

        public bool IsManualTaxPostings { get; set; }
    }
}
