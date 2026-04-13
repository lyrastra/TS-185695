using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryKbkDefaultsResponse
    {
        public BudgetaryPayerStatus PayerStatus { get; set; }

        public BudgetaryPaymentBase PaymentBase { get; set; }

        public BudgetaryPaymentType PaymentType { get; set; }

        public string DocDate { get; set; }

        public string DocNumber { get; set; }

        public BudgetaryRecipient Recipient { get; set; }

        public string Description { get; set; }
    }
}
