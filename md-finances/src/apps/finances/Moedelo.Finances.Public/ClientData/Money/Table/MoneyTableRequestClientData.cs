using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Public.ClientData.Money.Table
{
    public class MoneyTableRequestClientData
    {
        public int Count { get; set; } = 20;
        public int Offset { get; set; } = 0;
        public MoneySourceType SourceType { get; set; } = MoneySourceType.All;
        public long? SourceId { get; set; }
    }
}
