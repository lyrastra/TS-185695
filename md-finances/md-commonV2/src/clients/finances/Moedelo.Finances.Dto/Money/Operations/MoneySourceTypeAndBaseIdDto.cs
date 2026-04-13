using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Dto.Money.Operations
{
    public class MoneySourceTypeAndBaseIdDto
    {
        public MoneySourceType? MoneySourceType { get; set; }
        public long DocumentBaseId { get; set; }
    }
}