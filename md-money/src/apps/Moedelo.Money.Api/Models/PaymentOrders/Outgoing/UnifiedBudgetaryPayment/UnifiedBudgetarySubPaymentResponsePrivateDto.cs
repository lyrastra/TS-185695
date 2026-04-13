namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetarySubPaymentResponsePrivateDto
    {
        public long DocumentBaseId { get; set; }
        
        public long ParentDocumentId { get; set; }

        /// <summary>
        /// КБК
        /// </summary>
        public int KbkId { get; set; }

        /// <summary>
        /// Бюджетный период
        /// </summary>
        public BudgetaryPeriodResponseDto Period { get; set; }

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
    }
}
