using System;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Finances.Domain.Enums;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Enums.Money.Table;

namespace Moedelo.Finances.Public.ClientData.Money.Table.OutsourceProcessing
{
    public class OutsourceProcessingFilterClientData
    {
        public string Query { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public MoneySourceType SourceType { get; set; } = MoneySourceType.All;
        public long? SourceId { get; set; } = null;
        public MoneyContractorType KontragentType { get; set; } = MoneyContractorType.All;
        public int? KontragentId { get; set; }
        public MoneyDirection Direction { get; set; } = MoneyDirection.All;
        public string OperationType { get; set; }
        public MoneyTableFilterBudgetaryType? BudgetaryType { get; set; }
        public SumCondition SumCondition { get; set; } = SumCondition.Any;
        public decimal? Sum { get; set; }
        public decimal? SumFrom { get; set; }
        public decimal? SumTo { get; set; }
        public TaxationSystemType? TaxationSystemType { get; set; }
    }
}