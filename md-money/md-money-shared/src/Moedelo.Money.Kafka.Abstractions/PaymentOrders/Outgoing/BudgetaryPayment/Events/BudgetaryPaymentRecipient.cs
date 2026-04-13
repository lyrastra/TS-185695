namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Events
{
    /// <summary>
    /// Получатель бюджетного платежа
    /// </summary>
    public sealed class BudgetaryPaymentRecipient
    {
        // Получатель
        public string Name { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        // Казначейский счет
        public string SettlementAccount { get; set; }

        // Банк получателя (название)
        public string BankName { get; set; }
        
        public string BankBik { get; set; }

        // Единый казначейский счет
        public string BankCorrespondentAccount { get; set; }

        public string Okato { get; set; }

        public string Oktmo { get; set; }
    }
}
