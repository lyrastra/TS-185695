namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryRecipient
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
        public string UnifiedSettlementAccount { get; set; }

        public string Oktmo { get; set; }
    }
}