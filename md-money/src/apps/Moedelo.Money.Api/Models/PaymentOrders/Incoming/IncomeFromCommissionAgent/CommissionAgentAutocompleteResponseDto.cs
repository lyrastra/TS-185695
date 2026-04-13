namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    public class CommissionAgentAutocompleteResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public long? KontragentId { get; set; }

        public string SettlementAccount { get; set; }

        public string BankName { get; set; }

        public string BankBik { get; set; }
    }
}
