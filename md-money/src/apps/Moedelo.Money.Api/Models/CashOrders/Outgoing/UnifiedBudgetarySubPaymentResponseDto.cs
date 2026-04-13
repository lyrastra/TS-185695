using Moedelo.Money.Enums.CashOrders;

namespace Moedelo.Money.Api.Models.CashOrders.Outgoing
{
    public class UnifiedBudgetarySubPaymentResponseDto
    {
        /// <summary>
        /// Базовый идентификатор дочерней операции
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// КБК
        /// </summary>
        public UnifiedBudgetaryKbkResponseDto Kbk { get; set; }

        /// <summary>
        /// Бюджетный период
        /// </summary>
        public BudgetaryPeriodResponseDto Period { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        public long? PatentId { get; set; }

        /// <summary>
        /// Признак: проведено вручную в налоговом учете
        /// </summary>
        public bool TaxPostingsInManualMode { get; set; }
    }
}
