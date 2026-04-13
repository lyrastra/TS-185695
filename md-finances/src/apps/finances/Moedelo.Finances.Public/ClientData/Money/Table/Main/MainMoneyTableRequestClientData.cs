using System;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Finances.Domain.Enums;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Enums.Money.Table;

namespace Moedelo.Finances.Public.ClientData.Money.Table.Main
{
    public class MainMoneyTableRequestClientData
    {
        public int Count { get; set; } = 20;
        public int Offset { get; set; } = 0;
        public string Query { get; set; }
        public SortType SortType { get; set; } = SortType.Desc;
        public MoneyTableSortSortColumn SortColumn { get; set; } = MoneyTableSortSortColumn.Date;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public MoneySourceType SourceType { get; set; } = MoneySourceType.All;
        public long? SourceId { get; set; }
        public MoneyContractorType KontragentType { get; set; } = MoneyContractorType.All;
        public int? KontragentId { get; set; }
        public MoneyDirection Direction { get; set; } = MoneyDirection.All;
        public string OperationType { get; set; }
        public MoneyTableFilterBudgetaryType? BudgetaryType { get; set; }
        public SumCondition SumCondition { get; set; } = SumCondition.Any;
        public decimal? Sum { get; set; }
        public decimal? SumFrom { get; set; }
        public decimal? SumTo { get; set; }
        //public bool? IsSentToBank { get; set; }
        public bool? ProvideInTax { get; set; }
        public ClosingDocumentsCondition ClosingDocumentsCondition { get; set; } = ClosingDocumentsCondition.NoMatter;
        public TaxationSystemType? TaxationSystemType { get; set; }
        public long? PatentId { get; set; }

        // подтверждено сотрудником аутсорса
        public ApprovedCondition ApprovedCondition { get; set; } = ApprovedCondition.NoMatter;

    }
}
