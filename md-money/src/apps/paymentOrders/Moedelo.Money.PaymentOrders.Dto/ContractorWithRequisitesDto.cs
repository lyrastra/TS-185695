using Moedelo.Money.Enums;

namespace Moedelo.Money.PaymentOrders.Dto
{
    public class ContractorWithRequisitesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ContractorType Type { get; set; }

        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string SettlementAccount { get; set; }
        public string BankName { get; set; }
        public string BankBik { get; set; }
        public string BankCorrespondentAccount { get; set; }
    }
}
