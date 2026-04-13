using Moedelo.Common.Enums.Enums.Nds;

namespace Moedelo.Money.Kafka.Models
{
    public class Nds
    {
        public NdsTypes? NdsType { get; set; }

        public decimal? NdsSum { get; set; }
    }
}
