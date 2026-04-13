using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using Moedelo.Finances.Domain.Enums.Money.Table;

namespace Moedelo.Finances.Domain.Models.Money.Table.Filters
{
    public interface IOperationTypesFilters
    {
        OperationType[] OperationTypes { get; set; }
        SyntheticAccountCode? BudgetaryType { get; set; }
        MoneyTableExtraBudgetaryFilterType? ExtraBudgetaryType { get; set; }
    }
}