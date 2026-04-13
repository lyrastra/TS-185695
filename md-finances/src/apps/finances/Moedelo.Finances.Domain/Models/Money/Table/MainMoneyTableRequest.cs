using System;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using Moedelo.Finances.Domain.Enums;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Enums.Money.Table;
using Moedelo.Finances.Domain.Models.Money.Table.Filters;

namespace Moedelo.Finances.Domain.Models.Money.Table
{
    public class MainMoneyTableRequest : MoneyTableRequest,
        ISourceFilter,
        IQueryFilter,
        IKontragentFilters,
        IDirectionFilter,
        IOperationTypesFilters,
        ISumFilters
    {
        public string Query { get; set; }
        public SortType SortType { get; set; }
        public MoneyTableSortSortColumn SortColumn { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public MoneyContractorType KontragentType { get; set; }
        public int? KontragentId { get; set; }
        public MoneyDirection? Direction { get; set; }
        public OperationType[] OperationTypes { get; set; }
        public SyntheticAccountCode? BudgetaryType { get; set; }
        public MoneyTableExtraBudgetaryFilterType? ExtraBudgetaryType { get; set; }
        public int? PurseOperationType { get; set; }
        public SumCondition SumCondition { get; set; }
        public decimal? Sum { get; set; }
        public decimal? SumFrom { get; set; }
        public decimal? SumTo { get; set; }
        //public bool? IsSentToBank { get; set; }
        public bool? ProvideInTax { get; set; }
        public ClosingDocumentsCondition ClosingDocumentsCondition { get; set; }
        public bool IsMultiCurrency { get; set; }
        public TaxationSystemType? TaxationSystemType { get; set; }
        public long? PatentId { get; set; }

        // подтверждено сотрудником аутсорса
        public bool? IsApproved { get; set; }
    }
}
