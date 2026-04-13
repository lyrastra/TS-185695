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
    public class OutsourceProcessingTableRequest : MoneyTableRequest, 
        ISourceFilter,
        IQueryFilter,
        IKontragentFilters,
        IDirectionFilter,
        IOperationTypesFilters,
        ISumFilters
    {
        public string Query { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public MoneyContractorType KontragentType { get; set; }
        public int? KontragentId { get; set; }
        public MoneyDirection? Direction { get; set; }
        public OperationType[] OperationTypes { get; set; }
        public SyntheticAccountCode? BudgetaryType { get; set; }
        public MoneyTableExtraBudgetaryFilterType? ExtraBudgetaryType { get; set; }
        public SumCondition SumCondition { get; set; }
        public decimal? Sum { get; set; }
        public decimal? SumFrom { get; set; }
        public decimal? SumTo { get; set; }
    }
}