using Moedelo.Money.Enums;

namespace Moedelo.Money.Registry.Dto
{
    public class RegistryOperationContractorDto
    {
        public int? Id { get; set; }
        public ContractorType Type { get; set; }
        public string Name { get; set; }
    }
}
