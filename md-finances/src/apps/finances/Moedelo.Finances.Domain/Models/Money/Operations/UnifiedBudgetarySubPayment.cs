using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.KbkNumbers;

namespace Moedelo.Finances.Domain.Models.Money.Operations
{
    public class UnifiedBudgetarySubPayment
    {
        public long DocumentBaseId { get; set; }

        public int KbkId { get; set; }

        public string KbkNumber { get; set; }

        public KbkNumberType KbkType { get; set; }

        public int AccountCode { get; set; }

        public int? PeriodYear { get; set; }

        public int? PeriodNumber { get; set; }
        
        public BudgetaryPeriodType? PeriodType { get; set; }
        
        public string PeriodDate { get; set; }

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