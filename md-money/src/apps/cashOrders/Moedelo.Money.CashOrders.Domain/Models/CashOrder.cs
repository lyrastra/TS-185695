using Moedelo.Money.Enums;
using Moedelo.Money.Enums.CashOrders;
using System;

namespace Moedelo.Money.CashOrders.Domain.Models
{
    public class CashOrder
    {
        public long Id { get; set; }

        public MoneyDirection Direction { get; set; }

        public long DocumentBaseId { get; set; }

        public int FirmId { get; set; }

        public DateTime Date { get; set; }

        public long CashId { get; set; }

        public string Number { get; set; }

        public int? SalaryWorkerId { get; set; }

        public string Comments { get; set; }

        public string Destination { get; set; }

        public string DestinationName { get; set; }

        public decimal Sum { get; set; }

        public bool ProvideInAccounting { get; set; } = true;

        /// <summary>
        /// Режим виджета БУ/НУ "Учитывать вручную"
        /// </summary>
        public ProvidePostingType PostingsAndTaxMode { get; set; } = ProvidePostingType.Auto;

        /// <summary>
        /// Режим виджета НУ "Учитывать вручную"
        /// </summary>
        public ProvidePostingType TaxPostingType { get; set; } = ProvidePostingType.Auto;

        public decimal? PaidCardSum { get; set; }

        public OperationType OperationType { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public long? SyntheticAccountTypeId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public NdsType? NdsType { get; set; }

        public decimal? NdsSum { get; set; }

        public bool IncludeNds { get; set; }

        public string ZReportNumber { get; set; }

        public long? PatentId { get; set; }

        public int? KbkId { get; set; }

        /// <summary> Тип периода: ГД, ПЛ, КВ, МС </summary>
        public BudgetaryPeriodType? BudgetaryPeriodType { get; set; }

        /// <summary>
        /// Номер в периоде: для МС — номер месяца, КВ — номер кваратала, ПЛ – номер полугодия, для ГД — 0.
        /// </summary>
        public int? BudgetaryPeriodNumber { get; set; }

        /// <summary> Год платежа </summary>
        public int? BudgetaryPeriodYear { get; set; }

        public DateTime? BudgetaryPeriodDate { get; set; }

        public UnifiedBudgetaryAccountCodes? AccountCode { get; set; }
    }
}
