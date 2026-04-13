using Moedelo.Money.Enums;

namespace Moedelo.Money.Kafka.Abstractions.Models
{
    public class ContractorBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ContractorType Type { get; set; }
    }
}
