using Moedelo.CatalogV2.Dto.Kbk;
using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Finances.Dto.Money.Operations
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