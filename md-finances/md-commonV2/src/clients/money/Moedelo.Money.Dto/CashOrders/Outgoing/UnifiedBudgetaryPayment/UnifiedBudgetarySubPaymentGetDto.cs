namespace Moedelo.Money.Dto.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    /// <summary>
    /// Модель для чтения дочернего платежа для ЕНП
    /// </summary>
    public class UnifiedBudgetarySubPaymentGetDto
    {
        /// <summary>
        /// Базовый идентификатор документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// КБК
        /// </summary>
        public UnifiedBudgetaryKbkGetDto Kbk { get; set; }

        /// <summary>
        /// Бюджетный период
        /// </summary>
        public BudgetaryPeriodSaveDto Period { get; set; }

        /// <summary>
        /// Идентификатор патента
        /// </summary>
        public long? PatentId { get; set; }

        /// <summary>
        /// Признак: проведено вручную в налоговом учете
        /// </summary>
        public bool TaxPostingsInManualMode { get; set; }
    }
}