using Moedelo.Finances.Enums;

namespace Moedelo.Finances.ApiClient.Abstractions.Legacy.Dto
{
    public class MoneySourceBalanceDto
    {
        public long Id { get; set; }
        public MoneySourceType Type { get; set; }
        public decimal Balance { get; set; }
    }
}