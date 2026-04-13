using Moedelo.Common.Enums.Enums.Kontragents;

namespace Moedelo.KontragentsV2.Dto
{
    public class KontragentWithAccountingPaymentOrderDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public KontragentForm? Form { get; set; }

        public int? BankId { get; set; }

        public string BankName { get; set; }

        public string BankBik { get; set; }

        public string BankCorrespondentAccount { get; set; }

        public string SettlementAccount { get; set; }

        public int PaymentOrderCount { get; set; }
    }
}