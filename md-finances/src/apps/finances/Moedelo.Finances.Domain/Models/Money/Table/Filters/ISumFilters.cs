using Moedelo.Finances.Domain.Enums;

namespace Moedelo.Finances.Domain.Models.Money.Table.Filters
{
    public interface ISumFilters
    {
        SumCondition SumCondition { get; set; }
        decimal? Sum { get; set; }
        decimal? SumFrom { get; set; }
        decimal? SumTo { get; set; }
    }
}