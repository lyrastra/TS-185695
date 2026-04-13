namespace Moedelo.BankIntegrations.ApiClient.Dto.Payments
{
    public class OrderDetailsDto
    {
        public string Name { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string SettlementNumber { get; set; }

        public string BankName { get; set; }

        public string BankBik { get; set; }

        public string BankCorrespondentAccount { get; set; }

        public string BankCity { get; set; }

        public bool IsOoo { get; set; }

        public string Address { get; set; }

        public string Okato { get; set; }

        public string Oktmo { get; set; }

        public OrderDetailsDto()
        {
            Name = string.Empty;
            BankCorrespondentAccount = string.Empty;
            Address = string.Empty;
            Okato = string.Empty;
            BankBik = string.Empty;
            BankName = string.Empty;
            Inn = string.Empty;
            Kpp = string.Empty;
            SettlementNumber = string.Empty;
        }
    }
}
