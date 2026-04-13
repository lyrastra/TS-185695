using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Public.ClientData
{
    public class SourceClientData
    {
        public MoneySourceType SourceType { get; set; } = MoneySourceType.All;

        public long? SourceId { get; set; }
    }
}