using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Domain.Models.Money
{
    public class MoneySourceBase
    {
        public long Id { get; set; }

        public MoneySourceType Type { get; set; }

        public long? SubcontoId { get; set; }

        public bool IsPrimary { get; set; }
    }
}