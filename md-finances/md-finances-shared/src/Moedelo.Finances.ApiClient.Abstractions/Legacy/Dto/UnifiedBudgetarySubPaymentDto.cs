using Moedelo.Finances.Enums;

namespace Moedelo.Finances.ApiClient.Abstractions.Legacy.Dto
{
    public class UnifiedBudgetarySubPaymentDto
    {
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// КБК
        /// </summary>
        public KbkDto Kbk { get; set; }

        public int? PeriodYear { get; set; }

        public int? PeriodNumber { get; set; }
        
        public BudgetaryPeriodType? PeriodType { get; set; }
        
        /// <summary>
        /// Сумма платежа (7)
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