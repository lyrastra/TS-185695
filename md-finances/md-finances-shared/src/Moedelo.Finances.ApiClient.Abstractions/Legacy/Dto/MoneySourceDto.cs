using Moedelo.Finances.Enums;

namespace Moedelo.Finances.ApiClient.Abstractions.Legacy.Dto
{
    public class MoneySourceDto
    {
        public long Id { get; set; }
        public MoneySourceType Type { get; set; }
    }
}