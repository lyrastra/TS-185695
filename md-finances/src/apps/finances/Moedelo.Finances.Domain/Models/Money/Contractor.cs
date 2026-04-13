using Moedelo.Finances.Domain.Enums.Money;

namespace Moedelo.Finances.Domain.Models.Money
{
    public class Contractor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public MoneyContractorType Type { get; set; }

        public int Rating { get; set; }
    }
}