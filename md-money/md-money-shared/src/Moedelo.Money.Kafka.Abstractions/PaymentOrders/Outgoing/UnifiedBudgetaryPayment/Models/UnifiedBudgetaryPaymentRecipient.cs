namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Models
{
    public class UnifiedBudgetaryPaymentRecipient
    {
        public string Name { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string SettlementAccount { get; set; }

        public string BankName { get; set; }

        public string BankBik { get; set; }

        public string BankCorrespondentAccount { get; set; }

        public string Oktmo { get; set; }
    }
}
