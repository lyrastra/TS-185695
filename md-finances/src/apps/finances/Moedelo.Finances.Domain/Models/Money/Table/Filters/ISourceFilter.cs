using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Domain.Models.Money.Table.Filters
{
    public interface ISourceFilter
    {
        MoneySourceType SourceType { get; set; }
        long? SourceId { get; set; }
    }
}