using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.legacy.Finances.Dtos
{
    public class MoneySourceBalanceDto
    {
        public long Id { get; set; }
        
        public MoneySourceType Type { get; set; }
        
        public decimal Balance { get; set; }
    }
}
