using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Common.Domain.Models
{
    public class UnifiedBudgetarySubPayment
    {
        public long DocumentBaseId { get; set; }
        
        public long ParentDocumentId { get; set; }

        public decimal PaymentSum { get; set; }

        public int KbkNumberId { get; set; }

        /// <summary> Тип периода: ГД, ПЛ, КВ, МС </summary>
        public BudgetaryPeriodType PeriodType { get; set; }

        /// <summary>
        /// Номер в периоде: для МС — номер месяца, КВ — номер кваратала, ПЛ – номер полугодия, для ГД — 0.
        /// </summary>
        public int PeriodNumber { get; set; }

        /// <summary> Год платежа </summary>
        public int PeriodYear { get; set; }

        public DateTime? PeriodDate { get; set; }

        public ProvidePostingType TaxPostingType { get; set; }

        public long? PatentId { get; set; }

        public int? TradingObjectId { get; set; }
    }
}
