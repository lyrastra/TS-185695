using Moedelo.Common.Enums.Enums.Money;

namespace Moedelo.Money.Kafka.Models
{
    public class Contractor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string SettlementAccount { get; set; }

        public string BankName { get; set; }

        public string BankBik { get; set; }

        public string BankCorrespondentAccount { get; set; }

        public ContractorType Type { get; set; }

        public static Contractor Kontragent => new Contractor { Type = ContractorType.Kontragent };

        public static Contractor Worker => new Contractor { Type = ContractorType.Worker };
    }
}
