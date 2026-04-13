using Moedelo.Money.Enums;

namespace Moedelo.Money.Registry.Domain.Models
{
    public class Contractor
    {
        public int? Id { get; set; }
        public ContractorType Type { get; set; }
        public string Name { get; set; }
    }
}
