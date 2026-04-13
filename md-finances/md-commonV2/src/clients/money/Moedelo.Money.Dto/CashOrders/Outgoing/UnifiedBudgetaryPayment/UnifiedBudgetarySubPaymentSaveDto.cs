namespace Moedelo.Money.Dto.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    /// <summary>
    /// Модель для сохранения дочернего платежа для ЕНП
    /// </summary>
    public class UnifiedBudgetarySubPaymentSaveDto
    {
        /// <summary>
        /// Базовый идентификатор документа
        /// </summary>
        public long? DocumentBaseId { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Идентификатор КБК
        /// </summary>
        public int KbkId { get; set; }

        /// <summary>
        /// Бюджетный период
        /// </summary>
        public BudgetaryPeriodSaveDto Period { get; set; }

        /// <summary>
        /// Идентификатор патента
        /// </summary>
        public long? PatentId { get; set; }

        /// <summary>
        /// Параметры налогового учёта
        /// </summary>
        public TaxPostingsSaveDto TaxPostings { get; set; }
    }
}