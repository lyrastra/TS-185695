using Moedelo.Finances.Domain.Enums.Money;

namespace Moedelo.Finances.Domain.Models.Money.Table.Filters
{
    public interface IDirectionFilter
    {
        MoneyDirection? Direction { get; set; }
    }
}