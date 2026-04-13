using Moedelo.Finances.Domain.Enums.Money;

namespace Moedelo.Finances.Domain.Models.Money.Table.Filters
{
    public interface IKontragentFilters
    {
        MoneyContractorType KontragentType { get; set; }
        int? KontragentId { get; set; }
    }
}