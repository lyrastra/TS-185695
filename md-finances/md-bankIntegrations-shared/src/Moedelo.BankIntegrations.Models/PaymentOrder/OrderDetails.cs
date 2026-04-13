namespace Moedelo.BankIntegrations.Models.PaymentOrder
{
    public class OrderDetails
    {
        public string Name { get; set; } = string.Empty;

        public string Inn { get; set; } = string.Empty;

        public string Kpp { get; set; } = string.Empty;

        public string SettlementNumber { get; set; } = string.Empty;

        public string BankName { get; set; } = string.Empty;

        public string BankBik { get; set; } = string.Empty;

        public string BankCorrespondentAccount { get; set; } = string.Empty;

        public string BankCity { get; set; } = string.Empty;

        public bool IsOoo { get; set; }

        public string Address { get; set; } = string.Empty;

        public string Okato { get; set; } = string.Empty;

        public string Oktmo { get; set; } = string.Empty;
    }
}
