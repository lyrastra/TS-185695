using Moedelo.Finances.Domain.Enums.Money;

namespace Moedelo.Finances.Public.ClientData.Contractors
{
    public class ContractorClientData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public MoneyContractorType Type { get; set; }
    }
}