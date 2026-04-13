using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Domain.Models.Money.Operations
{
    public class MoneySourceBalance
    {
        public long Id { get; set; }
        public MoneySourceType Type { get; set; }
        public decimal Balance { get; set; }
    }
}
