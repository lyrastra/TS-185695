using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Domain.Models.Money.Table
{
    public class MoneyTableRequest
    {
        public int Count { get; set; }
        public int Offset { get; set; }
        public MoneySourceType SourceType { get; set; }
        public long? SourceId { get; set; }
    }
}
