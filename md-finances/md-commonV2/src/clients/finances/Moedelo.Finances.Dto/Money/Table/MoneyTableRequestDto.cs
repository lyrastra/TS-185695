using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Dto.Money.Table
{
    public class MoneyTableRequestDto
    {
        public int Count { get; set; } = 20;

        public int Offset { get; set; } = 0;

        public MoneySourceType SourceType { get; set; } = MoneySourceType.All;

        public long? SourceId { get; set; }
    }
}
