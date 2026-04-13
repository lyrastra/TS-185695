using Moedelo.Money.Enums;

namespace Moedelo.Money.Kafka.Abstractions.Models
{
    public class Nds
    {
        public NdsType? NdsType { get; set; }

        public decimal? NdsSum { get; set; }
    }
}
